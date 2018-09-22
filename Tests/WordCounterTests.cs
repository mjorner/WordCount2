using System;
using Xunit;
using CountWords;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace CountWords.Tests {
    public class WordCounterTests {
        [Fact]
        public void TestStringReader() {
            string original = "Programming is a \"Cool thing\"";
            using (var reader = WordCounter.CreateStringReader(original)) {
                TestReader(original, reader);
            }
        }

        [Fact]
        public void TestFileReader() {
            string original = "Programming is a \"Cool thing\"";
            string filePath = null;
            Assert.True(WriteTestFile(original, out filePath));
            using (var reader = WordCounter.CreateFileReader(filePath)) {
                TestReader(original, reader);
            }
        }

        private void TestReader(string original, ICharacterReader reader) {
            char ch = '\0';
            string back = "";
            while(reader.TryReadNextCharacter(out ch)) {
                back += ch;
            }
            Assert.True(original.CompareTo(back) == 0);
        }

        [Fact]
        public void TestFileRead() {
            string filePath = null;
            Assert.True(WriteTestFile("yes yes yes", out filePath));
            using (var reader = WordCounter.CreateFileReader(filePath)) {
                var result = WordCounter.CountWords(reader);
                Assert.True(result.Length == 1);
                Assert.True(result.First().Count == 3);
                Assert.True(result.First().Word.CompareTo("yes") == 0);
            }
        }

        private bool WriteTestFile(string s, out string filePath) {
            filePath = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}testfile.txt";
            try {
                if (File.Exists(filePath)) {
                    File.Delete(filePath);
                }
                using (StreamWriter sw = new StreamWriter(filePath)) {
                    sw.Write(s);
                }
                return true;
            }
            catch {

            }
            return false;
        }

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
        public void TestJapanese() {
            using (var reader = WordCounter.CreateStringReader("まあ。私たちはものをテストしています。")) {
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
        public void TestCustomEndingChars() {
            using (var reader = WordCounter.CreateStringReader("we^are^ok^with^hat^not space")) {
                var result = WordCounter.CountWords(reader, orderByDescending: false, customWordEndingChars: new []{'^'});
                Assert.True(result.Length == 6);
            }
        }

        [Fact]
        public void TestAddCustomEndingChars() {
            using (var reader = WordCounter.CreateStringReader("we^are^ok^with^hat^and space")) {
                List<char> customWordEndingChars = new List<char>(WordCounter.GetDefaultWordEndingChars());
                customWordEndingChars.Add('^');
                var result = WordCounter.CountWords(reader, orderByDescending: false, customWordEndingChars: customWordEndingChars);
                Assert.True(result.Length == 7);
            }
        }
    }
}
