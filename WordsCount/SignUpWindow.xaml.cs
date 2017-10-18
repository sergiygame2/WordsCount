using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using FontAwesome.WPF;
using WordsCount.Models;
using WordsCount.ViewModels;

namespace WordsCount
{
    /// <summary>
    /// Interaction logic for SignUpWindow.xaml
    /// </summary>
    public partial class SignUpWindow
    {
        private ImageAwesome _loader;

        public SignUpWindow()
        {
            WindowStyle = WindowStyle.None;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();

            _signUpViewModel = new SignUpViewModel(new User());
            _signUpViewModel.RequestClose += Close;
            _signUpViewModel.RequestLoader += OnRequestLoader;

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

        private void OnRequestLoader(bool isShow)
        {
            if (isShow && _loader == null)
            {
                _loader = new ImageAwesome();
                SignUpGrid.Children.Add(_loader);
                _loader.Icon = FontAwesomeIcon.Gear;
                _loader.Spin = true;
                Grid.SetRow(_loader, 2);
                Grid.SetColumn(_loader, 1);
                IsEnabled = false;
            }
            else if (_loader != null)
            {
                SignUpGrid.Children.Remove(_loader);
                _loader = null;
                IsEnabled = true;
            }
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
