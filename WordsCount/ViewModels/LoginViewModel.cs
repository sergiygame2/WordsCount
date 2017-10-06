using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using JetBrains.Annotations;
using WordsCount.Commands;
using WordsCount.Data;
using WordsCount.Helpers;
using WordsCount.Models;
using WordsCount.Services;

namespace WordsCount.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        private readonly User _userCandidate;
        private RelayCommand _signInCommand;
        private RelayCommand _signUpCommand;
        private RelayCommand _closeCommand;

        public LoginViewModel(User userCandidate)
        {
            _userCandidate = userCandidate;
        }

        public RelayCommand CloseCommand
        {
            get { return _closeCommand ?? (_closeCommand = new RelayCommand(obj => OnRequestClose(true))); }
        }

        public RelayCommand SignInCommand
        {
            get
            {
                return _signInCommand ?? (_signInCommand = new RelayCommand(SignIn,
                           o => !String.IsNullOrEmpty(Username) &&
                                !String.IsNullOrEmpty(Password)));
            }
        }

        public RelayCommand SignUpCommand => _signUpCommand ?? (_signUpCommand = new RelayCommand(SignUp));

        private void SignUp(object obj)
        {
            OnRequestClose(false);

            var signUpWindow = new SignUpWindow();
            signUpWindow.ShowDialog();
        }

        internal string Password
        {
            get => _userCandidate.HashPassword;
            set => _userCandidate.HashPassword = value;
        }

        public string Username
        {
            get => _userCandidate.UserName;
            set 
            {
                _userCandidate.UserName = value;
                OnPropertyChanged();
            }
        }

        private void SignIn(object obj)
        {
            // After some chackings, set the current user, and open cabinet window
            var currentUser = DbAdapter.Users.FirstOrDefault(user => user.UserName == Username &&
                                                                     user.HashPassword == DataHelper.Hash(Password));
            if (currentUser == null)
            {
                MessageBox.Show("Wrong Username or Password");
                return;
            }

            StationManager.CurrentUser = currentUser;
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
