using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("CountWords.Tests")]
namespace CountWords {
    internal sealed class WordCounterImpl {
        public bool OrderByDescending { get; set; } = true;
        
        public bool MatchCase { get; set; } = false;

        public readonly ImmutableHashSet<char> CustomWordEndingChars;

        private readonly ICharacterReader Reader;

        public WordCounterImpl(ICharacterReader reader, IEnumerable<char> customWordEndingChars) {
            Reader = reader ?? throw new ArgumentNullException(nameof(reader));
            CustomWordEndingChars = customWordEndingChars!=null ? ImmutableHashSet.Create<char>(customWordEndingChars.ToArray()) : null;
        }

        public IWordCount[] Parse() {
            WordCountContainer wordCounts = new WordCountContainer();

            Reader.ResetStream();
            StringBuilder stringBuilder = new StringBuilder();
            char character;
            while (Reader.TryReadNextCharacter(out character)) {
                if (!MatchCase) {
                    character = char.ToLower(character);
                }
                if (IsWordEndingCharacter(character)) {
                    string word = stringBuilder.ToString();
                    wordCounts.TryAddWord(word);
                    stringBuilder.Clear();
                }
                else {
                    stringBuilder.Append(character);
                }
            }
            
            wordCounts.TryAddWord(stringBuilder.ToString());

            if (OrderByDescending) {
                return wordCounts.OrderByDescending(x=> x.Count).ToArray();
            }
            else {
                return wordCounts.ToArray();
            }
        }

        private bool IsWordEndingCharacter(char character) {
            if (CustomWordEndingChars != null) {
                return CustomWordEndingChars.Contains(character);
            }
            return character.IsWordEndingCharacter();
        }
    }
}