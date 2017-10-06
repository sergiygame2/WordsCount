using WordsCount.Models;

namespace WordsCount.Services
{
    // Manager for working with logged-in user
    public static class StationManager
    {
        public static User CurrentUser { get; set; }
    }
}