using System;
using System.IO;

namespace CountWords {
    internal sealed class FileReader : ICharacterReader {
        private readonly StreamReader StreamReader;
        private FileReader(string path) {
            StreamReader = new StreamReader(path);
        }
        public static ICharacterReader Create(string path) {
            if (path == null) { throw new ArgumentNullException(nameof(path)); }
            return new FileReader(path);
        }
        public bool TryReadNextCharacter(out char character) {
            character = '\0';
            if (!StreamReader.EndOfStream) {
                character = (char)StreamReader.Read();
                return true;
            }
            return false;
        }
        public void Dispose() {
            if (StreamReader != null) {
                StreamReader.Dispose();
            }
        }
        public void ResetStream() {
            if (StreamReader.BaseStream.Position != 0) {
                StreamReader.DiscardBufferedData();
                StreamReader.BaseStream.Seek(0, SeekOrigin.Begin);
            }
        }
    }
}