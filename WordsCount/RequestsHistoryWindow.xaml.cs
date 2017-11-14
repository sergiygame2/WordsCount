using System;
using System.Windows;
using System.Windows.Input;
using WordsCount.ViewModels;

namespace WordsCount
{
    /// <summary>
    /// Interaction logic for RequestsHistoryWindow.xaml
    /// </summary>
    public partial class RequestsHistoryWindow : Window
    {
        public RequestsHistoryWindow()
        {
            WindowStyle = WindowStyle.None;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();

            var requestsHistoryViewModel = new RequestsHistoryViewModel();
            requestsHistoryViewModel.RequestClose += Close;

            DataContext = requestsHistoryViewModel;
        }
        
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
