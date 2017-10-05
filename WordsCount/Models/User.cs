using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using WordsCount.Helpers;

namespace WordsCount.Models
{
    [DataContract]
    public class User : Services.ISerializable
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string UserName { get; set; }
        [DataMember]
        public string FirstName { get; set; }
        [DataMember]
        public string LastName { get; set; }
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string HashPassword { get; set; }
        [DataMember]
        public DateTime LastVisit { get; set; }
        [DataMember]
        public List<TextRequest> TextRequests { get; set; }

        private static int _amount;

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

        public string FileName => "user.json";
    }
}
