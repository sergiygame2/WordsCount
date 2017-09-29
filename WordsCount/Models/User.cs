using System;
using System.Collections.Generic;
using WordsCount.Helpers;

namespace WordsCount.Models
{
    public class User
    {
        private static int _amount;
        public int Id { get; set; }
        public string UserName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public DateTime LastVisit { get; set; }
        public List<TextRequest> TextRequests { get; set; }

        static User() => _amount = 0;
        
        public User() { }

        public User(string userName, string firstName, string lastName, string email, string password)
        {
            Id = ++_amount;
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            HashPassword = DataHelper.Hash(password);
            LastVisit = DateTime.Now;
            TextRequests = new List<TextRequest>();
        }
    }
}
