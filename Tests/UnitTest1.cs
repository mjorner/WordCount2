using System;
using Xunit;
using CountWords;

namespace Tests {
    public class UnitTest1 {

        //What we need is a a much simpler external interface.
        //All Perhaps a Static function with either path as input.
        //Or the string directly.
        //Because now we externally need to create the specific StringReader to make this tests. And this is wrong.

        [Fact]
        public void Test1() {
            var s = new StringReader("Programming is a \"Cool thing\"");
            var result = new WordCounter(s, false).Parse();
            Assert.True(result.Length == 5);
        }

        [Fact]
        public void Test2() {
            var s = new StringReader("is \"is\" \'is\' is is");
            var result = new WordCounter(s, false).Parse();
            Assert.True(result.Length == 1);
        }

        [Fact]
        public void Test3() {
            var s = new StringReader("");
            var result = new WordCounter(s, false).Parse();
            Assert.True(result.Length == 0);
        }

        [Fact]
        public void Test4() {
            var s = new StringReader(",.,.,.,.,,,,. ..,, !!!!????");
            var result = new WordCounter(s, false).Parse();
            Assert.True(result.Length == 0);
        }
    }
}
