using System;
using System.Collections.Generic;
using System.Linq;
using WordsCount.Models;

namespace WordsCount.Data
{
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
            Users[0].TextRequests.Add(new TextRequest
            {
                Id = 1,
                Path = "d:\\docs\\dotnet.txt",
                CreatedAt = DateTime.Today,
                LinesAmount = 3,
                SymbolsAmount = 25,
                WordsAmount = 4
            });
            Users[0].TextRequests.Add(new TextRequest
            {
                Id = 2,
                Path = "d:\\docs\\index.txt",
                CreatedAt = DateTime.Today,
                LinesAmount = 3,
                SymbolsAmount = 25,
                WordsAmount = 4
            });
        }
    }
}
