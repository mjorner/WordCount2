using System;

namespace CountWords {
    public interface ICharacterReader : IDisposable {
        bool TryReadNextCharacter(out char character);
        
        void ResetStream();
    }
}