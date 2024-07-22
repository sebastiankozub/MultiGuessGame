namespace MultiGuess
{
    /// <summary>
    /// A guessing game.
    /// </summary>
    public interface IMultiplayerGuessingGame
    {
        /// <summary>
        /// Returns a list of partially or completely revealed game words.
        /// </summary>
        /// <returns>The game words.</returns>
        IList<string> GetGameStrings();

        /// <summary>
        /// Submits a guess against the game words.
        /// </summary>
        /// <param name="playerName">The player name.</param>
        /// <param name="submission">The guess.</param>
        /// <returns>The score that the guess produced.</returns>
        int SubmitGuess(string playerName, string submission);
    }
}