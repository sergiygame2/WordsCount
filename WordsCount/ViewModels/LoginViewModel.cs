using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using AppModels;
using AppServices.Helpers;
using AppServices.Services;
using JetBrains.Annotations;
using WordsCount.Commands;

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
            // sign in user in a separate thread
            // show loader (spinner) while executing validation & db commands
            OnRequestLoader(true);
            var result = await Task.Run(() =>
            {
                User currentUser = null;

                try
                {
                    currentUser = WordsCountServiceWrapper.GetUserByName(Username);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error on login");
                    return false;
                }
                
                if (currentUser == null || currentUser.HashPassword != DataHelper.Hash(Password))
                {
                    MessageBox.Show("Wrong Username or Password");
                    return false;
                }

                try
                {
                    currentUser.LastVisit = DateTime.Now;
                    WordsCountServiceWrapper.EditEntity(currentUser);
                }
                catch (Exception)
                {
                    MessageBox.Show("Error updating user");
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
