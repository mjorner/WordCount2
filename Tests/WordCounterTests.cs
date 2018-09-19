using System;
using Xunit;
using CountWords;
using System.Linq;
using System.IO;

namespace Tests {
    public class WordCounterTests {
        [Fact]
        public void TestQuotes() {
            using (var s = WordCounter.CreateStringReader("Programming is a \"Cool thing\"")) {
                var result = WordCounter.CountWords(s);
                Assert.True(result.Sum(x=> x.Count) == 5);
                Assert.True(result.Where(x=> x.Word.Contains("\"")).Count() == 0);
            }
        }

        [Fact]
        public void TestDuplicates() {
            using (var s = WordCounter.CreateStringReader("is \"is\" \'is\' is is")) {
                var result = WordCounter.CountWords(s);
                Assert.True(result.Length == 1);
                Assert.True(result.Sum(x=> x.Count) == 5);
            }
        }

        [Fact]
        public void TestEmptyString() {
            using (var s = WordCounter.CreateStringReader("")) {
                var result = WordCounter.CountWords(s);
                Assert.True(result.Length == 0);
            }
        }

        [Fact]
        public void TestNoWordsOnlySymbols() {
            using (var s = WordCounter.CreateStringReader(",.,.,.,.,,,,. ..,, !!!!????")) {
                var result = WordCounter.CountWords(s);
                Assert.True(result.Length == 0);
            }
        }

        [Fact]
        public void TestMixed() {
            using (var s = WordCounter.CreateStringReader("---WORLD-CHAMPION---")) {
                var result = WordCounter.CountWords(s);
                Assert.True(result.Length == 2);
            }
        }

        [Fact]
        public void TestNumeronym() {
            using (var s = WordCounter.CreateStringReader("K8S is cool. P2P is also cool")) {
                var result = WordCounter.CountWords(s);
                Assert.True(result.Length == 5);
            }
        }

        [Fact]
        public void TestCapitalLettersNoMatchCase() {
            using (var s = WordCounter.CreateStringReader("YES yes")) {
                var result = WordCounter.CountWords(s);
                Assert.True(result.Length == 1);
            }
        }

        [Fact]
        public void TestCapitalLettersMatchCase() {
            using (var s = WordCounter.CreateStringReader("YES yes")) {
                var result = WordCounter.CountWords(s, matchCase:true);
                Assert.True(result.Length == 2);
            }
        }

        [Fact]
        public void TestOrderDescendingAsDefault() {
            using (var s = WordCounter.CreateStringReader("one two two")) {
                var result = WordCounter.CountWords(s);
                Assert.True(result.Length == 2);
                Assert.True(result.First().Count == 2);
                Assert.True(result.First().Word.CompareTo("two") == 0);
            }
        }

        [Fact]
        public void TestNoOrder() {
            using (var s = WordCounter.CreateStringReader("one two two")) {
                var result = WordCounter.CountWords(s, orderByDescending: false);
                Assert.True(result.Length == 2);
                Assert.True(result.First().Count == 1);
                Assert.True(result.First().Word.CompareTo("one") == 0);
            }
        }

        [Fact]
        public void TestFileRead() {
            string filePath = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}testfile.txt";
            if (File.Exists(filePath)) {
                File.Delete(filePath);
            }
            using (StreamWriter sw = new StreamWriter(filePath)) {
                sw.WriteLine("yes yes yes");
            }
            using (var s = WordCounter.CreateFileReader(filePath)) {
                var result = WordCounter.CountWords(s);
                Assert.True(result.Length == 1);
                Assert.True(result.First().Count == 3);
                Assert.True(result.First().Word.CompareTo("yes") == 0);
            }
        }
    }
}
