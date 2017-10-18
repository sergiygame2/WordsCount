using System;
using System.Linq;
using System.Threading.Tasks;

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

        // if already counted just return a value, else count in a thread
        public async Task<int> CountSymbolsAsync()
        {
            if (_symbolsCount != 0)
            {
                return _symbolsCount;
            }

            return await Task.Run(() =>
            {
                // count all chars except some special
                _symbolsCount = _text.Count(c => !SymbolsDelimeters.Contains(c));
                
                return _symbolsCount;
            });
        }

        public async Task<int> CountWordsAsync()
        {
            if (_wordsCount != 0)
            {
                return _wordsCount;
            }

            return await Task.Run(() =>
            {
                // split text by special characters to retrieve array of words, then count it's amount
                _wordsCount = _text.Split(WordsDelimeters, StringSplitOptions.RemoveEmptyEntries).Length;

                return _wordsCount;
            });
        }

        public async Task<int> CountLinesAsync()
        {
            if (_linesCount != 0)
            {
                return _linesCount;
            }

            return await Task.Run(() =>
            {
                // split text by special line characters
                _linesCount = _text.Split(LinesDelimeters).Length;

                return _linesCount;
            });
        }
    }
}