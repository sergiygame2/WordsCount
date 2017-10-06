using System.Collections.Generic;
using WordsCount.Models;

namespace WordsCount.Data
{
    // Static DB. Initializes list of users each time the app is started
    public static class DbAdapter
    {
        public static List<User> Users { get; set; }
        
        static DbAdapter()
        {
            Users = new List<User>
            {
                new User("lost", "Sergiy", "Sukharskyi", "sergiy@gmail.com", "somepass"), 
                new User("lbodia", "Bodia", "Liba", "liba@gmail.com", "somepass2")
            };
            Users[0].TextRequests.Add(new TextRequest("d:\\docs\\dotnet.txt", 26, 6, 4));
            Users[0].TextRequests.Add(new TextRequest("d:\\docs\\index.txt", 15, 5, 2));
        }
    }
}
