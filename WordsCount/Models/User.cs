using System;
using System.Text;
using WordsCount.Helpers;

namespace WordsCount.Models
{
    public class User
    {
        private static int _amount;
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string HashPassword { get; set; }
        public DateTime LastVisit { get; set; }

        static User() => _amount = 0;
        
        public User(string firstName, string lastName, string email, string password)
        {
            Id = ++_amount;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            HashPassword = DataHelper.Hash(password);
            LastVisit = DateTime.Now;
        }
    }


}
