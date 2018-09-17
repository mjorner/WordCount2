using System;

namespace CountWords {
    public sealed class WordCount {
        public readonly string Word;
        public int Count { get; private set;}
        public WordCount(string word) {
            if (word == null) { throw new ArgumentNullException(nameof(word)); }
            Word = word;
            Count = 1;
        }
        public void IncrementCount() {
            Count++;
        }
    }
}