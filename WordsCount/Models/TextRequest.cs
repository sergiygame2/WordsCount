using System;
using WordsCount.Services;

namespace WordsCount.Models
{
    public class TextRequest
    {
        private static int _amount;
        public int Id { get; set; }
        public string Path { get; set; }
        public int SymbolsAmount { get; set; }
        public int WordsAmount { get; set; }
        public int LinesAmount { get; set; }
        public DateTime CreatedAt { get; set; }
        // properties for DB relation
        // public int UserId { get; set; } 
        // public User User { get; set; }

        static TextRequest() => _amount = 0;

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
    }
}
