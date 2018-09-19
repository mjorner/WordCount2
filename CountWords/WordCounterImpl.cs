using System;
using System.Collections.Generic;
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
            Dictionary<string, WordCount> wordCounts = new Dictionary<string, WordCount>();

            Reader.ResetStream();
            StringBuilder stringBuilder = new StringBuilder();
            char character;
            while (Reader.TryReadNextCharacter(out character)) {
                if (!MatchCase) {
                    character = char.ToLower(character);
                }
                if (character.IsWordEndingCharacter()) {
                    string word = stringBuilder.ToString();
                    TryAddWord(wordCounts, word);
                    stringBuilder.Clear();
                }
                else {
                    stringBuilder.Append(character);
                }
            }
            
            TryAddWord(wordCounts, stringBuilder.ToString());

            if (OrderByDescending) {
                return wordCounts.OrderByDescending(x=> x.Value.Count).Select(x=> x.Value).ToArray();
            }
            else {
                return wordCounts.Values.ToArray();
            }
        }

        //What we could do is to encapsulate this part in its own class.
        //With a custom TryAddWord.
        private static void TryAddWord(IDictionary<string, WordCount> wordCounts, string word) {
            if (word.IsValidWord()) {
                WordCount wordCount;
                if (wordCounts.TryGetValue(word, out wordCount)) {
                    wordCount.IncrementCount();
                }
                else {
                    wordCounts[word] = new WordCount(word);
                }
            }
        }
    }
}