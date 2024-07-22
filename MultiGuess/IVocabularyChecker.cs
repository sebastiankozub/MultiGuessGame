namespace MultiGuess
{
    /// <summary>
    /// Checks words against an allowed vocabulary.
    /// </summary>
    public interface IVocabularyChecker
    {
        /// <summary>
        /// Returns whether the word is allowed.
        /// </summary>
        bool Exists(string word);
    }
}