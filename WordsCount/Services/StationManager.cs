using System.Runtime.Serialization;
using WordsCount.Models;

namespace WordsCount.Services
{
    [DataContract]
    public class StationManager : ISerializable
    {
        public string FileName { get; set; } = "currentUser.json";

        public static User CurrentUser { get; set; }

        [DataMember]
        public int? LoggedInUserId { get; set; }

        public StationManager() { }

        public StationManager(int? id)
        {
            LoggedInUserId = id;
        }
    }
}