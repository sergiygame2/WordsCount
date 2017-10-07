using System;
using System.Windows;
using WordsCount.Data;
using WordsCount.Models;
using WordsCount.Services;

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
            var user = SerializeManager.Deserialize<User>(StationManager.UserFilePath);

            // In case the previous function returned new User()
            if (!String.IsNullOrEmpty(user?.UserName))
            {
                StationManager.CurrentUser = user;

                try
                {
                    // If user already exists in static list of users, remove it
                    if (DbAdapter.Users.Exists(u => u.UserName == user.UserName))
                    {
                        // Such user could be only one
                        DbAdapter.Users.RemoveAll(u => u.UserName == user.UserName);
                    }
                    // Insert new or updated user
                    DbAdapter.Users.Add(user);
                }
                catch (Exception e)
                {
                    Logger.Log("Error after initial user deserialization. Cuoldn't add or remove user", e);
                }

                Logger.Log($"User {StationManager.CurrentUser?.UserName} was autologged-in");

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
