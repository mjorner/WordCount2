using System;
using System.Collections.Generic;
using CountWords;


namespace CountWords.CLI {
    public sealed class Program {
        public static void Main(string[] args) {
            Console.Write("Enter path: ");
            string path = Console.ReadLine();
            try {
                using (ICharacterReader reader = WordCounter.CreateFileReader(path)) {
                    IWordCount[] wordCounts = WordCounter.CountWords(reader);   
                    PrintWordCounts(wordCounts);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            
            try {
                Console.Write("Enter string:");
                string str = Console.ReadLine();
                using (ICharacterReader reader = WordCounter.CreateStringReader(str)) {
                    IWordCount[] wordCounts = WordCounter.CountWords(reader);
                    PrintWordCounts(wordCounts);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            
        }
        private static void PrintWordCounts(IWordCount[] wordCounts) {
            long totalWordCount = 0;
            foreach(var word in wordCounts) {
                Console.WriteLine(word);
                totalWordCount += word.Count;
            }
            Console.WriteLine($"Total word count: {totalWordCount}");
        }
    }
}
