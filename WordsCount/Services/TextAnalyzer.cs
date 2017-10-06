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
            // Special characters for text splitting
            LinesDelimeters = new[] {'\n'};
            SymbolsDelimeters = new[] {' ', '\r', '\n'};
            WordsDelimeters = new[] { ' ', '.', ',', '?', '!', '\r', '\n' };
        }

        public TextAnalyzer(string text) => _text = text.Trim();

        // if already counted just return a value, else count
        public int CountSymbols()
        {
            if (_symbolsCount == 0)
            {
                // count all chars except some special
                _symbolsCount = _text.Count(c => !SymbolsDelimeters.Contains(c));
            }

            return _symbolsCount;
        }

        public int CountWords()
        {
            if (_wordsCount == 0)
            {
                // split text by special characters to retrieve array of words, then count it's amount
                _wordsCount = _text.Split(WordsDelimeters, StringSplitOptions.RemoveEmptyEntries).Length;
            }

            return _wordsCount;
        }

        public int CountLines()
        {
            if (_linesCount == 0)
            {
                // split text by special line characters
                _linesCount = _text.Split(LinesDelimeters).Length;
            }

            return _linesCount;
        }
    }
}