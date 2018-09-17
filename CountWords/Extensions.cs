using System.Linq;
using System.Collections.Generic;

namespace CountWords {
    internal static class Extensions {
        public static bool IsValidWord(this string word) {
            return word.Any() && word.All(x=> char.IsLetter(x));
        }
        public static bool IsWordEndingCharacter(this char character) {
            return char.IsSeparator(character) || char.IsPunctuation(character) || char.IsControl(character);
        }
    }
}