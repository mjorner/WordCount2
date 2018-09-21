using System;
namespace CountWords {
    internal sealed class StringReader : ICharacterReader {
        private readonly string SourceString;
        private int Position;
        public StringReader(string str) {
            if (str == null) { throw new ArgumentNullException(nameof(str)); }
            SourceString = str;
            Position = 0;
        }
        public bool TryReadNextCharacter(out char character) {
            character = '\0';
            if (Position < SourceString.Length) {
                character = SourceString[Position++];
                return true;
            }
            return false;
        }
        public void Dispose() {
        }
        public void ResetStream() {
            Position = 0;
        }
    }
}