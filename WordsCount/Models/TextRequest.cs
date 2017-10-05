using System;
using System.Runtime.Serialization;
using WordsCount.Services;

namespace WordsCount.Models
{
    [DataContract]
    public class TextRequest : Services.ISerializable
    {
        private static int _amount;
        [DataMember]
        public int Id { get; set; }
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
        // properties for DB relation
        // public int UserId { get; set; } 
        // public User User { get; set; }


        static TextRequest() => _amount = 0;

        public TextRequest() { }

        public TextRequest(string path, int symbolsAmount, int wordsAmount, int linesAmount)
        {
            Id = ++_amount;
            Path = path;
            SymbolsAmount = symbolsAmount;
            WordsAmount = wordsAmount;
            LinesAmount = linesAmount;
            CreatedAt = DateTime.Now;
        }

        public TextRequest(string path, TextAnalyzer textAnalyzer)
        {
            Id = ++_amount;
            Path = path;
            SymbolsAmount = textAnalyzer.CountSymbols();
            WordsAmount = textAnalyzer.CountWords();
            LinesAmount = textAnalyzer.CountLines();
            CreatedAt = DateTime.Now;
        }

        public string FileName => "textRequests.json";
    }
}
