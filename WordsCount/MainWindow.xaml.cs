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
            AppDomain.CurrentDomain.ProcessExit += OnExit;
        }

        private static void OnExit(object obj, EventArgs a)
        {
            MessageBox.Show("Salut!");
            Environment.Exit(0);
        }
    }
}
