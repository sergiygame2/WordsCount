using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using WordsCount.Data;
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
            var sm = SerializeManager.Deserialize<StationManager>("currentUser.json");
            if (sm?.LoggedInUserId != null && sm.LoggedInUserId != 0)
            {
                StationManager.CurrentUser = DbAdapter.Users.SingleOrDefault(u => u.Id == sm.LoggedInUserId);
                var textRequestsWindow = new TextRequestsWindow();
                textRequestsWindow.ShowDialog();
            }
            else
            {
                var loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
            }
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
