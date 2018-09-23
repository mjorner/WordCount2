using System.Linq;
using System.Collections.Generic;
namespace CountWords {
    public static class WordCounter {
        public static ICharacterReader CreateStringReader(string str) {
            return new CountWords.StringReader(str);
        }

        public static ICharacterReader CreateFileReader(string path) {
            return FileReader.Create(path);
        }

        public static IWordCount[] CountWords(ICharacterReader reader, bool matchCase = false, bool orderByDescending = true, IEnumerable<char> customWordEndingChars = null) {
            var counter = new WordCounterImpl(reader, customWordEndingChars);
            counter.MatchCase = matchCase;
            counter.OrderByDescending = orderByDescending;
            return counter.Parse();
        }

        public static ICollection<char> GetDefaultWordEndingChars() {
            return Enumerable.Range(char.MinValue, (char.MaxValue-char.MinValue)+1).Select(x=> (char)x).Where(x=> x.IsWordEndingCharacter()).ToArray();
        }
    }
}