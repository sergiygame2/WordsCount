using System;
using System.Windows;
using WordsCount.Models;
using WordsCount.ViewModels;

namespace WordsCount
{
    /// <summary>
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow
    {
        public LoginWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            InitializeComponent();
            _loginViewModel = new LoginViewModel(new User());
            _loginViewModel.RequestClose += Close;
            DataContext = _loginViewModel;        
        }

        private readonly LoginViewModel _loginViewModel;

        private void Password_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            _loginViewModel.Password = Password.Password;
        }
        
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
    }
}
