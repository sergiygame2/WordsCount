using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using JetBrains.Annotations;
using WordsCount.Commands;
using WordsCount.Data;
using WordsCount.Models;
using WordsCount.Services;
using System.Collections.Generic;

namespace WordsCount.ViewModels
{
    internal class SignUpViewModel : INotifyPropertyChanged
    {
        private readonly User _userCandidate;
        private RelayCommand _signInCommand;
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

        public RelayCommand SignInCommand => _signInCommand ?? (_signInCommand = new RelayCommand(SignIn));

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

        private void SignIn(object obj)
        {
            OnRequestClose(false);

            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }

        private void SignUp(object obj)
        {
            // using list of function & error message if function returned false
            var validationFields = new List<KeyValuePair<bool, string>>
            {
                new KeyValuePair<bool, string>(Validator.IsExistingUsername(Username), "User with such username already exists!"),
                new KeyValuePair<bool, string>(Validator.IsEmail(Email), "Email is not valid!"),
                new KeyValuePair<bool, string>(Validator.IsString(FirstName), "Firstname must contain only letters!"),
                new KeyValuePair<bool, string>(Validator.IsString(LastName), "Lastname must contain only letters!"),
                new KeyValuePair<bool, string>(Validator.IsPasswordMatch(Password, RepeatedPassword), "Passwords do not match!"),
            };

            // iterating validation funcs and displaying error message
            foreach (var field in validationFields)
            {
                if (field.Key == false)
                {
                    MessageBox.Show(field.Value);
                    return;
                }
            }

            var currentUser = new User(Username, FirstName, LastName, Email, Password);

            // If user is valid, add him to database (static list)
            DbAdapter.Users.Add(currentUser);
            StationManager.CurrentUser = currentUser;

            // writing logs (what current user have just done)
            Logger.Log($"User {StationManager.CurrentUser?.UserName} signed-up");
            SerializeManager.Serialize(currentUser);

            MessageBox.Show("You have successfully signed-up!");
            OnRequestClose(false);

            var textRequestsWindow = new TextRequestsWindow();
            textRequestsWindow.ShowDialog();
        }

        internal event CloseHandler RequestClose;
        public delegate void CloseHandler(bool isQuitApp);

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void OnRequestClose(bool isquitapp)
        {
            RequestClose?.Invoke(isquitapp);
        }
    }
}
