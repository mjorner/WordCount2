using System;

namespace CountWords {
    internal sealed class WordCount : IWordCount {
        public string Word { get; private set; }
        public int Count { get; private set;}
        
        public WordCount(string word) {
            if (word == null) { throw new ArgumentNullException(nameof(word)); }
            Word = word;
            Count = 1;
        }
        
        public void IncrementCount() {
            Count++;
        }

        public override string ToString() {
            return $"Word \"{Word}\" : {Count}";
        }
    }
}