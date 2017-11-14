using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using AppModels;
using AppServices.Services;
using JetBrains.Annotations;
using WordsCount.Commands;

namespace WordsCount.ViewModels
{
    internal class RequestsHistoryViewModel : INotifyPropertyChanged
    {
        private RelayCommand _openMainWindowCommand;

        public RelayCommand OpenRequestsCommand => _openMainWindowCommand ?? (_openMainWindowCommand = new RelayCommand(obj => OnRequestClose()));

        public List<TextRequest> UserTextRequests { get; set; }

        public RequestsHistoryViewModel()
        {
            // Set this property when initialized, to show current user requests on form
            // using mvvm approach and list box
            try
            {
                UserTextRequests = WordsCountServiceWrapper.GetTextRequests(StationManager.CurrentUser.Id);
            }
            catch (Exception e)
            {
                Logger.Log($"Error getting user {StationManager.CurrentUser} text requests", e);
            }
        }

        internal event CloseHandler RequestClose;
        public delegate void CloseHandler();

        protected virtual void OnRequestClose()
        {
            RequestClose?.Invoke();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
