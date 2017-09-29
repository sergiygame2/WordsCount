using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using WordsCount.Commands;

namespace WordsCount.ViewModels
{
    internal class RequestsHistoryViewModel : INotifyPropertyChanged
    {
        private RelayCommand _openMainWindowCommand;

        public RelayCommand OpenRequestsCommand => _openMainWindowCommand ?? (_openMainWindowCommand = new RelayCommand(OpenRequestsWindow));

        private void OpenRequestsWindow(object obj)
        {
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

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
