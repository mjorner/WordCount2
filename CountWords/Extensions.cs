using System.Linq;

namespace CountWords {
    internal static class Extensions {
        public static bool IsValidWord(this string word) {
            return word.Any() && word.Any(x=> char.IsLetter(x));
        }
        public static bool IsWordEndingCharacter(this char character) {
            if (character == '-') { return false;}
            return char.IsSeparator(character) || char.IsPunctuation(character) || char.IsControl(character);
        }
    }
}