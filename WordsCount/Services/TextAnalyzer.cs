using System;
using System.Linq;

namespace WordsCount.Services
{
    public class TextAnalyzer
    {
        private readonly string _text;
        private static readonly char[] SymbolsDelimeters;
        private static readonly char[] WordsDelimeters;
        private static readonly char[] LinesDelimeters;
        
        static TextAnalyzer()
        {
            LinesDelimeters = new[] {'\n'};
            SymbolsDelimeters = new[] {' ', '\r', '\n'};
            WordsDelimeters = new[] { ' ', '.', ',', '?', '!', '\r', '\n' };
        }

        public TextAnalyzer(string text) => _text = text.Trim();

        public int CountSymbols() => _text.Count(c => !SymbolsDelimeters.Contains(c));

        public int CountWords() => _text.Split(WordsDelimeters, StringSplitOptions.RemoveEmptyEntries).Length;

        public int CountLines() => _text.Split(LinesDelimeters).Length;
    }
}