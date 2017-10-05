using System.Runtime.Serialization;
using WordsCount.Models;

namespace WordsCount.Services
{
    [DataContract]
    public class StationManager
    {
        public static User CurrentUser { get; set; }

        public static readonly string UserFilePath;

        static StationManager()
        {
            UserFilePath = "user.json";
        }
    }
}