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
    /// Interaction logic for LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow
    {
        private ImageAwesome _loader;

        public LoginWindow()
        {
            // Configuring style of window
            WindowStyle = WindowStyle.None;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            InitializeComponent();

            _loginViewModel = new LoginViewModel(new User());
            _loginViewModel.RequestClose += Close;
            _loginViewModel.RequestLoader += OnRequestLoader;

            DataContext = _loginViewModel;        
        }

        private readonly LoginViewModel _loginViewModel;

        private void Password_OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            _loginViewModel.Password = Password.Password;
        }

        private void OnRequestLoader(bool isShow)
        {
            if (isShow && _loader == null)
            {
                _loader = new ImageAwesome();
                LoginGrid.Children.Add(_loader);
                _loader.Icon = FontAwesomeIcon.Gear;
                _loader.Spin = true;
                Grid.SetRow(_loader, 1);
                Grid.SetColumn(_loader, 1);
                IsEnabled = false;
            }
            else if (_loader != null)
            {
                LoginGrid.Children.Remove(_loader);
                _loader = null;
                IsEnabled = true;
            }
        }
        
        // According to bool variable close form or all app
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
