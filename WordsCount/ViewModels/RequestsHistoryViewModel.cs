using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using WordsCount.Commands;
using WordsCount.Models;
using WordsCount.Services;

namespace WordsCount.ViewModels
{
    internal class RequestsHistoryViewModel : INotifyPropertyChanged
    {
        private RelayCommand _openMainWindowCommand;

        public RelayCommand OpenRequestsCommand => _openMainWindowCommand ?? (_openMainWindowCommand = new RelayCommand(OpenRequestsWindow));

        public List<TextRequest> UserTextRequests { get; set; }

        public RequestsHistoryViewModel()
        {
            // Set this property when initialized, to show current user requests on form
            // using mvvm approach and list box
            UserTextRequests = StationManager.CurrentUser.TextRequests;
        }

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
