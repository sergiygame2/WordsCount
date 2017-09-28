using System.Collections.Generic;
using WordsCount.Models;

namespace WordsCount.Data
{
    public static class DBAdapter
    {
        public static List<User> Users { get; set; }

        static DBAdapter()
        {
            Users.Add(new User("Sergiy", "Sukharskyi", "sergiy@gmail.com", "somepass"));
            Users.Add(new User("Bodia", "Liba", "liba@gmail.com", "somepass2"));
        }
    }
}
