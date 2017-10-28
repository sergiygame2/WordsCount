using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;
using WordsCount.Helpers;
using WordsCount.Services;

namespace WordsCount.Models
{
    [DataContract]
    public class User : Services.ISerializable
    {
        [DataMember]
        public Guid Id { get; set; }
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
        
        public string FileName => StationManager.UserFilePath;
    }

    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            HasKey(u => u.Id);

            Property(u => u.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
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
