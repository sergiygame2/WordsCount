using AppModels;

namespace WordsCount
{
    // Manager for working with logged-in user
    public static class StationManager
    {
        public static User CurrentUser { get; set; }

        public static readonly string UserFilePath;

        // TODO FILE PATH
        static StationManager()
        {
            UserFilePath = "user.json";
        }
    }
}
