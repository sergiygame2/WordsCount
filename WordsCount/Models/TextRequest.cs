using System;

namespace WordsCount.Models
{
    public class TextRequest
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public int SymbolsAmount { get; set; }
        public int WordsAmount { get; set; }
        public int LinesAmount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
