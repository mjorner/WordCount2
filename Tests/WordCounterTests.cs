using System;
using Xunit;
using CountWords;
using System.Linq;
using System.IO;

namespace CountWords.Tests {
    public class WordCounterTests {
        [Fact]
        public void TestQuotes() {
            using (var reader = WordCounter.CreateStringReader("Programming is a \"Cool thing\"")) {
                var result = WordCounter.CountWords(reader);
                Assert.True(result.Sum(x=> x.Count) == 5);
                Assert.True(result.Where(x=> x.Word.Contains("\"")).Count() == 0);
            }
        }

        [Fact]
        public void TestDuplicatesWithQuotes() {
            using (var reader = WordCounter.CreateStringReader("is \"is\" \'is\' is is")) {
                var result = WordCounter.CountWords(reader);
                Assert.True(result.Length == 1);
                Assert.True(result.Sum(x=> x.Count) == 5);
            }
        }

        [Fact]
        public void TestEmptyString() {
            using (var reader = WordCounter.CreateStringReader("")) {
                var result = WordCounter.CountWords(reader);
                Assert.True(result.Length == 0);
            }
        }

        [Fact]
        public void TestNoWordsOnlySymbols() {
            using (var reader = WordCounter.CreateStringReader(",.,.,.,.,,,,. ..,, !!!!????")) {
                var result = WordCounter.CountWords(reader);
                Assert.True(result.Length == 0);
            }
        }

        [Fact]
        public void TestHyphens() {
            using (var reader = WordCounter.CreateStringReader("an up-to-date account")) {
                var result = WordCounter.CountWords(reader);
                Assert.True(result.Length == 3);
            }
        }

        [Fact]
        public void TestOnlyNumbers() {
            using (var reader = WordCounter.CreateStringReader("1 2 3,4, 7777")) {
                var result = WordCounter.CountWords(reader);
                Assert.True(result.Length == 0);
            }
        }

        [Fact]
        public void TestNumeronym() {
            using (var reader = WordCounter.CreateStringReader("K8S is cool. P2P is also cool")) {
                var result = WordCounter.CountWords(reader);
                Assert.True(result.Length == 5);
            }
        }

        [Fact]
        public void TestCapitalLettersNoMatchCase() {
            using (var reader = WordCounter.CreateStringReader("YES yes")) {
                var result = WordCounter.CountWords(reader);
                Assert.True(result.Length == 1);
            }
        }

        [Fact]
        public void TestCapitalLettersMatchCase() {
            using (var reader = WordCounter.CreateStringReader("YES yes")) {
                var result = WordCounter.CountWords(reader, matchCase:true);
                Assert.True(result.Length == 2);
            }
        }

        [Fact]
        public void TestOrderDescendingAsDefault() {
            using (var reader = WordCounter.CreateStringReader("one two two")) {
                var result = WordCounter.CountWords(reader);
                Assert.True(result.Length == 2);
                Assert.True(result.First().Count == 2);
                Assert.True(result.First().Word.CompareTo("two") == 0);
            }
        }

        [Fact]
        public void TestNoOrder() {
            using (var reader = WordCounter.CreateStringReader("one two two")) {
                var result = WordCounter.CountWords(reader, orderByDescending: false);
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
            using (var reader = WordCounter.CreateFileReader(filePath)) {
                var result = WordCounter.CountWords(reader);
                Assert.True(result.Length == 1);
                Assert.True(result.First().Count == 3);
                Assert.True(result.First().Word.CompareTo("yes") == 0);
            }
        }
    }
}
