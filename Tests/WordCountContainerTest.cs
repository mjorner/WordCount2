using System;
using Xunit;
using CountWords;
using System.Linq;

namespace CountWords.Tests {
    public class WordCountContainerTest {
        [Fact]
        public void TestAddNoWordNumbers() {
            WordCountContainer container = new WordCountContainer();
            container.TryAddWord("123.5");
            Assert.True(!container.Any());
        }

        [Fact]
        public void TestAddNoWordEmpty() {
            WordCountContainer container = new WordCountContainer();
            container.TryAddWord("");
            Assert.True(!container.Any());
        }

        [Fact]
        public void TestAddNoWordOk() {
            WordCountContainer container = new WordCountContainer();
            container.TryAddWord("test");
            Assert.True(container.Any());
        }
    }
}