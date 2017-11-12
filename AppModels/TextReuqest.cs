using System;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;
using ISerializable = AppServices.Services.ISerializable;

namespace AppModels
{
    [DataContract]
    public class TextRequest : ISerializable
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
        [DataMember]
        public Guid UserId { get; set; }
        [DataMember]
        public User User { get; set; }

        public string FileName => "textRequests.json";

        public TextRequest() { }

        public TextRequest(string filePath, int symbolsAmount, int wordsAmount, int linesAmount, Guid userId)
        {
            Id = Guid.NewGuid();
            Path = filePath;
            SymbolsAmount = symbolsAmount;
            WordsAmount = wordsAmount;
            LinesAmount = linesAmount;
            CreatedAt = DateTime.Now;
            UserId = userId;
        }

        public class TextRequestMap : EntityTypeConfiguration<TextRequest>
        {
            // Rules for Text Request table columns
            public TextRequestMap()
            {
                HasKey(tr => tr.Id);

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
}
