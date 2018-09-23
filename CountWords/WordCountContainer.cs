using System;
using System.Collections;
using System.Collections.Generic;

namespace CountWords {
    internal sealed class WordCountContainer : IEnumerable<WordCount> {
        private readonly Dictionary<string, WordCount> WordCounts = new Dictionary<string, WordCount>();

        public void TryAddWord(string word) {
            if (word == null) { throw new ArgumentNullException(nameof(word)); }
            if (word.IsValidWord()) {
                WordCount wordCount;
                if (WordCounts.TryGetValue(word, out wordCount)) {
                    wordCount.IncrementCount();
                }
                else {
                    WordCounts[word] = new WordCount(word);
                }
            }
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public IEnumerator<WordCount> GetEnumerator() {
            return WordCounts.Values.GetEnumerator();
        }
    }
}