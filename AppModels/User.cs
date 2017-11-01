using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;
using AppServices.Helpers;
using ISerializable = AppServices.Services.ISerializable;

namespace AppModels
{
    [DataContract]
    public class User : ISerializable
    {
        [DataMember]
        private Guid Id { get; set; }
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

        public User()
        {
            
        }

        public User(string userName, string firstName, string lastName, string email, string password)
        {
            Id = Guid.NewGuid();
            UserName = userName;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            HashPassword = DataHelper.Hash(password);
            LastVisit = DateTime.Now;
            TextRequests = new List<TextRequest>();
        }

        public Guid GetUserId() => Id;

        public string FileName => "user.json";
        
        public class UserMap : EntityTypeConfiguration<User>
        {
            public UserMap()
            {
                HasKey(u => u.Id);

                Property(u => u.UserName).IsRequired();
                Property(u => u.FirstName).IsRequired();
                Property(u => u.LastName).IsRequired();
                Property(u => u.Email).IsRequired();
                Property(u => u.HashPassword).IsRequired();
                Property(u => u.LastVisit).IsRequired();

                HasMany(u => u.TextRequests)
                    .WithRequired(tr => tr.User)
                    .HasForeignKey(tr => tr.UserId)
                    .WillCascadeOnDelete();

                ToTable("users");
            }
        }
    }
}
