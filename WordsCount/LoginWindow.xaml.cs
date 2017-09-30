using System;
using System.Windows;
using System.Windows.Input;
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
            WindowStyle = WindowStyle.None;
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
                MessageBox.Show("Salut!");
                Environment.Exit(0);
            }
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}
