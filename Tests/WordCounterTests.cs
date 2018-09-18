using System;
using Xunit;
using CountWords;
using System.Linq;

namespace Tests {
    public class WordCounterTests {
        [Fact]
        public void TestQuotes() {
            var s = new StringReader("Programming is a \"Cool thing\"");
            var result = new WordCounter(s).Parse();
            Assert.True(result.Sum(x=> x.Count) == 5);
            Assert.True(result.Where(x=> x.Word.Contains("\"")).Count() == 0);
        }

        [Fact]
        public void TestDuplicates() {
            var s = new StringReader("is \"is\" \'is\' is is");
            var result = new WordCounter(s).Parse();
            Assert.True(result.Length == 1);
            Assert.True(result.Sum(x=> x.Count) == 5);
        }

        [Fact]
        public void TestEmptyString() {
            var s = new StringReader("");
            var result = new WordCounter(s).Parse();
            Assert.True(result.Length == 0);
        }

        [Fact]
        public void TestNoWordsOnlySymbols() {
            var s = new StringReader(",.,.,.,.,,,,. ..,, !!!!????");
            var result = new WordCounter(s).Parse();
            Assert.True(result.Length == 0);
        }

        [Fact]
        public void TestCapitalLettersAndMatchCase1() {
            var s = new StringReader("YES yes");
            var result = new WordCounter(s).Parse();
            Assert.True(result.Length == 1);
        }

        [Fact]
        public void TestCapitalLettersAndMatchCase2() {
            var s = new StringReader("YES yes");
            var wordCounter = new WordCounter(s);
            wordCounter.MatchCase = true;
            var result = wordCounter.Parse();
            Assert.True(result.Length == 2);
        }

        [Fact]
        public void TestOrder() {
            var s = new StringReader("one two two");
            var wordCounter = new WordCounter(s);
            var result = wordCounter.Parse();
            Assert.True(result.Length == 2);
            Assert.True(result.First().Count == 2);
        }

        [Fact]
        public void TestNoOrder() {
            var s = new StringReader("one two two");
            var wordCounter = new WordCounter(s);
            wordCounter.OrderByDescending = false;
            var result = wordCounter.Parse();
            Assert.True(result.Length == 2);
            Assert.True(result.First().Count == 1);
        }
    }
}
