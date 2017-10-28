using System;
using System.Linq;
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
            // deserializing user when app starts
            // if such users exists do autologin else redirect to login
            var user = SerializeManager.Deserialize<User>(StationManager.UserFilePath);

            // In case the previous function returned new User()
            if (!String.IsNullOrEmpty(user?.UserName))
            {
                using (var dbContext = new AppDbContext())
                {
                    StationManager.CurrentUser = dbContext.Users.SingleOrDefault(u => u.Id == user.Id);
                }

                // writing logs (what current user have just done)
                Logger.Log($"User {StationManager.CurrentUser?.UserName} was autologged-in");

                var textRequestsWindow = new TextRequestsWindow();
                textRequestsWindow.ShowDialog();
            }
            else
            {
                var loginWindow = new LoginWindow();
                loginWindow.ShowDialog();
            }
        }
    }
}
