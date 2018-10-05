using System;
using System.Diagnostics;

//dotnet publish -c Release -r win10-x64 --self-contained -o d:/countw/
namespace CountWords.CLI {
    public sealed class Program {
        public static void Main(string[] args) {
            Stopwatch sw = new Stopwatch();
            Console.Write("Enter path: ");
            string path = Console.ReadLine();
            try {
                sw.Reset();
                sw.Start();
                using (ICharacterReader reader = WordCounter.CreateFileReader(path)) {
                    IWordCount[] wordCounts = WordCounter.CountWords(reader);
                    sw.Stop();  
                    PrintWordCounts(wordCounts, sw);

                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
            
            try {
                Console.Write("Enter string:");
                string str = Console.ReadLine();
                sw.Restart();
                using (ICharacterReader reader = WordCounter.CreateStringReader(str)) {
                    IWordCount[] wordCounts = WordCounter.CountWords(reader);
                    sw.Stop();
                    PrintWordCounts(wordCounts, sw);
                }
            }
            catch (Exception e) {
                Console.WriteLine(e.Message);
            }
        }
        private static void PrintWordCounts(IWordCount[] wordCounts, Stopwatch sw) {
            long totalWordCount = 0;
            foreach(var word in wordCounts) {
                Console.WriteLine(word);
                totalWordCount += word.Count;
            }
            Console.WriteLine($"Total word count: {totalWordCount}");
            Console.WriteLine($"Text parsed in {sw.Elapsed.Milliseconds}ms");
        }
    }
}
