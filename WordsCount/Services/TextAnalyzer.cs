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

        private int _symbolsCount;
        private int _wordsCount;
        private int _linesCount;

        static TextAnalyzer()
        {
            LinesDelimeters = new[] {'\n'};
            SymbolsDelimeters = new[] {' ', '\r', '\n'};
            WordsDelimeters = new[] { ' ', '.', ',', '?', '!', '\r', '\n' };
        }

        public TextAnalyzer(string text) => _text = text.Trim();

        public int CountSymbols()
        {
            if (_symbolsCount == 0)
            {
                _symbolsCount = _text.Count(c => !SymbolsDelimeters.Contains(c));
            }

            return _symbolsCount;
        }

        public int CountWords()
        {
            if (_wordsCount == 0)
            {
                _wordsCount = _text.Split(WordsDelimeters, StringSplitOptions.RemoveEmptyEntries).Length;
            }

            return _wordsCount;
        }

        public int CountLines()
        {
            if (_linesCount == 0)
            {
                _linesCount = _text.Split(LinesDelimeters).Length;
            }

            return _linesCount;
        }
    }
}