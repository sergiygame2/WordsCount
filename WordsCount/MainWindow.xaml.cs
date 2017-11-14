using System;
using AppModels;
using AppServices.Services;
using DbAdapter;

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
            var userNameIsEmpty = String.IsNullOrEmpty(user?.UserName);

            // In case the previous function returned new User()
            if (!userNameIsEmpty)
            {
                try
                {
                    StationManager.CurrentUser = WordsCountServiceWrapper.GetUserByName(user.UserName);
                }
                catch (Exception e)
                {
                    Logger.Log("Error getting user", e);
                }

                if (StationManager.CurrentUser != null)
                {
                    // writing logs (what current user have just done)
                    Logger.Log($"User {StationManager.CurrentUser?.UserName} was autologged-in");

                    try
                    {
                        StationManager.CurrentUser.LastVisit = DateTime.Now;
                        WordsCountServiceWrapper.EditEntity(StationManager.CurrentUser);
                    }
                    catch (Exception e)
                    {
                        Logger.Log("Error updating user", e);
                    }

                    GoToCabinet();
                }
                else
                {
                    Logger.Log("Error on autologin");
                    Login();
                }
            }
            else
            {
                Login();
            }
        }

        private static void Login()
        {
            var loginWindow = new LoginWindow();
            loginWindow.ShowDialog();
        }

        private static void GoToCabinet()
        {
            var textRequestsWindow = new TextRequestsWindow();
            textRequestsWindow.ShowDialog();
        }
    }
}
