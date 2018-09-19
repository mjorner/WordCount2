using System;
using System.Linq;
using System.Text;

namespace CountWords {
    internal sealed class WordCounterImpl {
        public bool OrderByDescending { get; set; } = true;
        public bool MatchCase { get; set; } = false;
        private readonly ICharacterReader Reader;

        public WordCounterImpl(ICharacterReader reader) {
            if (reader == null) { throw new ArgumentNullException(nameof(reader)); }
            Reader = reader;
        }

        public IWordCount[] Parse() {
            WordCountContainer wordCounts = new WordCountContainer();

            Reader.ResetStream();
            StringBuilder stringBuilder = new StringBuilder();
            char character;
            while (Reader.TryReadNextCharacter(out character)) {
                if (!MatchCase) {
                    character = char.ToLower(character);
                }
                if (character.IsWordEndingCharacter()) {
                    string word = stringBuilder.ToString();
                    wordCounts.TryAddWord(word);
                    stringBuilder.Clear();
                }
                else {
                    stringBuilder.Append(character);
                }
            }
            
            wordCounts.TryAddWord(stringBuilder.ToString());

            if (OrderByDescending) {
                return wordCounts.OrderByDescending(x=> x.Count).ToArray();
            }
            else {
                return wordCounts.ToArray();
            }
        }
    }
}