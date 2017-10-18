using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
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

        private async void SignIn(object obj)
        {
            OnRequestLoader(true);
            var result = await Task.Run(() =>
            {
                Thread.Sleep(1000);

                // After some chackings, set the current user, and open cabinet window
                var currentUser = DbAdapter.Users.FirstOrDefault(user => user.UserName == Username &&
                                                                         user.HashPassword == DataHelper.Hash(Password));
                if (currentUser == null)
                {
                    MessageBox.Show("Wrong Username or Password");
                    return false;
                }

                // setting curren user & serializing user on login
                StationManager.CurrentUser = currentUser;
                SerializeManager.Serialize(currentUser);

                return true;
            });
            OnRequestLoader(false);

            if (!result) return;
            OnRequestClose(false);

            // writing logs (what current user have just done)
            Logger.Log($"User {StationManager.CurrentUser?.UserName} logged in");

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
