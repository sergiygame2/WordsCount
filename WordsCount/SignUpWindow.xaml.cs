using System;
using System.Windows;
using System.Windows.Input;
using WordsCount.Models;
using WordsCount.ViewModels;

namespace WordsCount
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow
    {
        public SignUpWindow()
        {
            WindowStyle = WindowStyle.None;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();

            _signUpViewModel = new SignUpViewModel(new User());
            _signUpViewModel.RequestClose += Close;

            DataContext = _signUpViewModel;
        }

        private readonly SignUpViewModel _signUpViewModel;

        private void Password_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            _signUpViewModel.Password = Password.Password;
        }

        private void Password_OnRepeatedPasswordChanged(object sender, RoutedEventArgs e)
        {
            _signUpViewModel.RepeatedPassword = RepeatedPassword.Password;
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
            DragMove();
        }
    }
}
