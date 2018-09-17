using System;
using System.Collections.Generic;
using CountWords;


namespace CountWords {
    public sealed class Program {

        //This is also wrong!
        //WordCount et al should be in a specific class lib.
        //And this should only expose a simple interface.

        public static void Main(string[] args) {
            Console.Write("Enter path: ");
            string path = Console.ReadLine();
            try {
                using (ICharacterReader reader = FileReader.Create(path)) {
                    WordCount[] wordCounts = new WordCounter(reader, false).Parse();   
                    PrintWordCounts(wordCounts);
                }

                Console.Write("Enter string:");
                string str = Console.ReadLine();
                using (ICharacterReader reader = new CountWords.StringReader(str)) {
                    WordCount[] wordCounts = new WordCounter(reader, false).Parse();  
                    PrintWordCounts(wordCounts);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        private static void PrintWordCounts(WordCount[] wordCounts) {
            long totalWordCount = 0;
            foreach(var word in wordCounts) {
                Console.WriteLine($"Word \"{word.Word}\": {word.Count}");
                totalWordCount += word.Count;
            }
            Console.WriteLine($"Total word count: {totalWordCount}");
        }
    }
}
