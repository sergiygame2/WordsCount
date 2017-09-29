using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using WordsCount.Commands;

namespace WordsCount.ViewModels
{
    internal class TextRequestsViewModel : INotifyPropertyChanged
    {
        private RelayCommand _logOutCommand;
        private RelayCommand _myTextRequestsCommand;
        private RelayCommand _exitCommand;
    
        public RelayCommand LogOutCommand => _logOutCommand ?? (_logOutCommand = new RelayCommand(LogOut));

        public RelayCommand MyTextRequestsCommand => _myTextRequestsCommand ?? (_myTextRequestsCommand = new RelayCommand(OpenTextRequests));

        public RelayCommand ExitCommand => _exitCommand ?? (_exitCommand = new RelayCommand(obj => OnRequestClose(true)));

        private void LogOut(object obj)
        {
            OnRequestClose(false);
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }

        private void OpenTextRequests(object obj)
        {
            OnRequestClose(false);
            var requestsHistoryWindow = new RequestsHistoryWindow();
            requestsHistoryWindow.ShowDialog();
        }

        internal event CloseHandler RequestClose;
        public delegate void CloseHandler(bool isQuitApp);

        protected virtual void OnRequestClose(bool isquitapp)
        {
            RequestClose?.Invoke(isquitapp);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
