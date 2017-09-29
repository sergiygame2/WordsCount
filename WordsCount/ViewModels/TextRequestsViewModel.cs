using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Microsoft.Win32;
using WordsCount.Commands;

namespace WordsCount.ViewModels
{
    internal class TextRequestsViewModel : INotifyPropertyChanged
    {
        private RelayCommand _logOutCommand;
        private RelayCommand _myTextRequestsCommand;
        private RelayCommand _openFileCommand;
        private RelayCommand _exitCommand;
    
        public RelayCommand LogOutCommand => _logOutCommand ?? (_logOutCommand = new RelayCommand(LogOut));

        public RelayCommand MyTextRequestsCommand => _myTextRequestsCommand ?? (_myTextRequestsCommand = new RelayCommand(OpenTextRequests));

        public RelayCommand OpenFileCommand => _openFileCommand ?? (_openFileCommand = new RelayCommand(OpenFile));

        public RelayCommand ExitCommand => _exitCommand ?? (_exitCommand = new RelayCommand(obj => OnRequestClose(true)));

        private void LogOut(object obj)
        {
            OnRequestClose(false);
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }

        private void OpenFile(object obj)
        {
            var openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                // TODO ADD FILE SERVICE HERE
                var filePath = openFileDialog.FileName;
                var fileText = File.ReadAllText(filePath);
                OnRequestFillPath(filePath);
                OnRequestFillText(fileText);
            }
        }

        private void OpenTextRequests(object obj)
        {
            OnRequestClose(false);
            var requestsHistoryWindow = new RequestsHistoryWindow();
            requestsHistoryWindow.ShowDialog();
        }

        internal event FillTextHandler RequestFillText;
        public delegate void FillTextHandler(string text);

        protected virtual void OnRequestFillText(string text)
        {
            RequestFillText?.Invoke(text);
        }

        internal event FillPathHandler RequestFillPath;
        public delegate void FillPathHandler(string path);

        protected virtual void OnRequestFillPath(string path)
        {
            RequestFillPath?.Invoke(path);
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
