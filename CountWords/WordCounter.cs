namespace CountWords {
    public static class WordCounter {
        public static ICharacterReader CreateStringReader(string str) {
            return new CountWords.StringReader(str);
        }

        public static ICharacterReader CreateFileReader(string path) {
            return FileReader.Create(path);
        }

        public static IWordCount[] CountWords(ICharacterReader reader, bool matchCase = false, bool orderByDescending = true) {
            var counter = new WordCounterImpl(reader);
            counter.MatchCase = matchCase;
            counter.OrderByDescending = orderByDescending;
            return counter.Parse();
        }
    }
}