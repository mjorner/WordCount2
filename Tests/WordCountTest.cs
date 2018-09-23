using System;
using Xunit;
using CountWords;
using System.Linq;

namespace CountWords.Tests {
    public class WordCountTest {
        [Fact]
        public void TestWordCountIncrement() {
            WordCount count = new WordCount("test");
            Assert.Equal<int>(1, count.Count);
            count.IncrementCount();
            Assert.Equal<int>(2, count.Count);
        }
    }
}