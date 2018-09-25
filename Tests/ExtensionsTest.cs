using System;
using Xunit;
using CountWords;
using System.Linq;

namespace CountWords.Tests {
    public class ExtensionsTest {
        [Fact]
        public void TestNotValidWord1() {
            string s = "";
            Assert.False(s.IsValidWord());
        }

        [Fact]
        public void TestNotValidWord2() {
            string s = "111";
            Assert.False(s.IsValidWord());
        }

        [Fact]
        public void TestNotValidWord3() {
            string s = "...";
            Assert.False(s.IsValidWord());
        }

        [Fact]
        public void TestIsValidWord1() {
            string s = "test";
            Assert.True(s.IsValidWord());
        }

        [Fact]
        public void TestIsValidWord2() {
            string s = "k8s";
            Assert.True(s.IsValidWord());
        }

        [Fact]
        public void TestIsValidWord3() {
            string s = "stop-light";
            Assert.True(s.IsValidWord());
        }

        [Fact]
        public void TestIsEndOfWordChar() {
            char ch = '.';
            Assert.True(ch.IsWordEndingCharacter());
        }

        [Fact]
        public void TestIsEndOfWordChar2() {
            char ch = ' ';
            Assert.True(ch.IsWordEndingCharacter());
        }

        [Fact]
        public void TestIsNotEndOfWordChar1() {
            char ch = 'a';
            Assert.False(ch.IsWordEndingCharacter());
        }

        [Fact]
        public void TestIsNotEndOfWordChar2() {
            char ch = '-';
            Assert.False(ch.IsWordEndingCharacter());
        }
    }
}