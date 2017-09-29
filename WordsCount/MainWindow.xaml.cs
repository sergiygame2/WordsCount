using System;
using System.Windows;
using WordsCount.Services;
using WordsCount.ViewModels;

namespace WordsCount
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
            InitializeComponent();
            UserNameLabel.Content = StationManager.CurrentUser.UserName;
            _mainViewModel = new MainViewModel();
            _mainViewModel.RequestClose += Close;
            DataContext = _mainViewModel;
            AppDomain.CurrentDomain.ProcessExit += OnExit;
        }

        private readonly MainViewModel _mainViewModel;

        private void Close(bool isQuitApp)
        {
            if (!isQuitApp)
            {
                Close();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        private static void OnExit(object obj, EventArgs a)
        {
            MessageBox.Show("Salut!");
            Environment.Exit(0);
        }
    }
}
