﻿using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using JetBrains.Annotations;
using WordsCount.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AppModels;
using AppServices.Services;

namespace WordsCount.ViewModels
{
    internal class SignUpViewModel : INotifyPropertyChanged
    {
        private readonly User _userCandidate;
        private RelayCommand _backToSignInCommand;
        private RelayCommand _signUpCommand;
        private RelayCommand _closeCommand;

        public SignUpViewModel(User userCandidate)
        {
            _userCandidate = userCandidate;
        }

        public RelayCommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(obj => OnRequestClose(true))); }
        }

        public RelayCommand BackToSignInCommand => _backToSignInCommand ?? (_backToSignInCommand = new RelayCommand(BackToSignIn));

        public RelayCommand SignUpCommand
        {
            get
            {
                // Checking to disable SignUp button untill all fields are filled
                return _signUpCommand ?? (_signUpCommand = new RelayCommand(SignUp,
                           o => !String.IsNullOrEmpty(Username) &&
                                !String.IsNullOrEmpty(FirstName) &&
                                !String.IsNullOrEmpty(LastName) &&
                                !String.IsNullOrEmpty(Email) &&
                                !String.IsNullOrEmpty(Password) &&
                                !String.IsNullOrEmpty(RepeatedPassword)));
            }
        }
        
        internal string Password
        {
            get => _userCandidate.HashPassword;
            set => _userCandidate.HashPassword = value;
        }

        internal string RepeatedPassword { get; set; }

        public string Username
        {
            get => _userCandidate.UserName;
            set
            {
                _userCandidate.UserName = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _userCandidate.Email;
            set
            {
                _userCandidate.Email = value;
                OnPropertyChanged();
            }
        }

        public string FirstName
        {
            get => _userCandidate.FirstName;
            set
            {
                _userCandidate.FirstName = value;
                OnPropertyChanged();
            }
        }

        public string LastName
        {
            get => _userCandidate.LastName;
            set
            {
                _userCandidate.LastName = value;
                OnPropertyChanged();
            }
        }

        private void BackToSignIn(object obj)
        {
            OnRequestClose(false);

            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }

        private async void SignUp(object obj)
        {
            var errorsCollector = new List<(string, Exception)>();

            OnRequestLoader(true);
            
            // sign up user in a separate thread
            // show loader (spinner) while executing validation & db commands
            var result = await Task.Run(() =>
            {
                var userNameExists = false;

                try
                {
                    userNameExists = WordsCountServiceWrapper.IsExistingUsername(Username);
                }
                catch (Exception e)
                {
                    errorsCollector.Add(("Error checking if username exists", e));
                    return false;
                }

                // using list of function & error message if function returned false
                var validationFields = new List<KeyValuePair<bool, string>>
                {
                    new KeyValuePair<bool, string>(userNameExists, "User with such username already exists!"),
                    new KeyValuePair<bool, string>(Validator.IsEmail(Email), "Email is not valid!"),
                    new KeyValuePair<bool, string>(Validator.IsString(FirstName),
                        "Firstname must contain only letters!"),
                    new KeyValuePair<bool, string>(Validator.IsString(LastName), "Lastname must contain only letters!"),
                    new KeyValuePair<bool, string>(Validator.IsPasswordMatch(Password, RepeatedPassword),
                        "Passwords do not match!"),
                };

                // iterating validation funcs and displaying error message
                foreach (var field in validationFields)
                {
                    if (field.Key == false)
                    {
                        MessageBox.Show(field.Value);
                        return false;
                    }
                }

                var currentUser = new User(Username, FirstName, LastName, Email, Password);

                try
                {
                    // If user is valid, add him to database
                    WordsCountServiceWrapper.AddEntity(currentUser);
                }
                catch (Exception e)
                {
                    errorsCollector.Add(("Error adding user", e));
                    return false;
                }

                StationManager.CurrentUser = currentUser;
                SerializeManager.Serialize(currentUser);
                
                return true;
            });
            OnRequestLoader(false);

            if (!result)
            {
                if (!errorsCollector.Any()) return;

                foreach (var error in errorsCollector)
                {
                    Logger.Log(error.Item1, error.Item2);
                }

                MessageBox.Show("Error on login. Try again please");
                return;
            }

            // writing logs (what current user have just done)
            Logger.Log($"User {StationManager.CurrentUser?.UserName} signed-up");
            OnRequestClose(false);

            var textRequestsWindow = new TextRequestsWindow();
            textRequestsWindow.ShowDialog();
        }

        internal event CloseHandler RequestClose;
        public delegate void CloseHandler(bool isQuitApp);

        protected virtual void OnRequestClose(bool isquitapp)
        {
            RequestClose?.Invoke(isquitapp);
        }


        internal event LoaderHandler RequestLoader;
        public delegate void LoaderHandler(bool isShow);

        protected virtual void OnRequestLoader(bool isShow)
        {
            RequestLoader?.Invoke(isShow);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
