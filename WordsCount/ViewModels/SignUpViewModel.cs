﻿using System;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
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
                return _signUpCommand ?? (_signUpCommand = new RelayCommand(SignUp,
                           o => !String.IsNullOrEmpty(Username) &&
                                !String.IsNullOrEmpty(FirstName) &&
                                !String.IsNullOrEmpty(LastName) &&
                                !String.IsNullOrEmpty(Email) &&
                                !String.IsNullOrEmpty(Password)));
            }
        }
        
        internal string Password
        {
            get => _userCandidate.HashPassword;
            set => _userCandidate.HashPassword = DataHelper.Hash(value);
        }

        private string _repeatedPassword;
        internal string RepeatedPassword
        {
            get => _repeatedPassword;
            set => _repeatedPassword = DataHelper.Hash(value);
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
            if (DbAdapter.Users.Any(user => user.UserName == Username))
            {
                MessageBox.Show("User with this username already exists");
                return;
            }
            if (DbAdapter.Users.Any(user => user.Email == Email))
            {
                MessageBox.Show("User with this email already exists");
                return;
            }
            if (!IsValid(Email))
            {
                MessageBox.Show("Invalid email");
                return;
            }
            if (RepeatedPassword != Password)
            {
                MessageBox.Show("Passwords missmatch");
                return;
            }
            var currentUser = new User(Username, FirstName, LastName, Email, Password);
            DbAdapter.Users.Add(currentUser);
            StationManager.CurrentUser = currentUser;
            MessageBox.Show("You have successfully signed-up and entered just now");
            OnRequestClose(false);
        }

        internal event CloseHandler RequestClose;
        public delegate void CloseHandler(bool isQuitApp);

        private static bool IsValid(string emailaddress)
        {
            try
            {
                var mailAddress = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

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