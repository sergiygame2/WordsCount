using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;

namespace WordsCount.Models
{
    [DataContract]
    public class TextRequest : Services.ISerializable
    {
        [DataMember]
        public Guid Id { get; set; }
        [DataMember]
        public string Path { get; set; }
        [DataMember]
        public int SymbolsAmount { get; set; }
        [DataMember]
        public int WordsAmount { get; set; }
        [DataMember]
        public int LinesAmount { get; set; }
        [DataMember]
        public DateTime CreatedAt { get; set; }

        public Guid UserId { get; set; } 
        public User User { get; set; }

        public string FileName => "textRequests.json";
    }

    public class TextRequestMap : EntityTypeConfiguration<TextRequest>
    {
        public TextRequestMap()
        {
            HasKey(tr => tr.Id);

            Property(tr => tr.Id).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(tr => tr.Path).IsRequired();
            Property(tr => tr.SymbolsAmount).IsRequired();
            Property(tr => tr.WordsAmount).IsRequired();
            Property(tr => tr.LinesAmount).IsRequired();
            Property(tr => tr.CreatedAt).IsRequired();

            HasRequired(tr => tr.User)
                .WithMany(u => u.TextRequests)
                .HasForeignKey(tr => tr.UserId);

            ToTable("text_requests");
        }
    }
}
