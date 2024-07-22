using System.Net;
using System.Text;

namespace MultiGuess
{
    internal class MultiplayerGuessingGame
    {
        private readonly List<string> _playerNames;
        private readonly List<string> _gameWords;
        private readonly Dictionary<string,string> _gameStrings = new Dictionary<string, string>();

        private readonly VocabularyChecker _vocabularyChecker;
        private readonly Dictionary<string, uint> _playersScores = new Dictionary<string, uint>();

        public MultiplayerGuessingGame(List<string> playerNames, List<string> gameWords, VocabularyChecker vocabularyChecker)
        {
            _playerNames = playerNames;
            _gameWords = gameWords;
            _vocabularyChecker = vocabularyChecker;

            InitilizePlayers();
            InitializeGameWords();
            InitializeGameStrings();
        }

        public void InitilizePlayers()
        {
            if(_playerNames==null || _playerNames.Count < 1)
                throw new ArgumentException("List of playerNames cannot be null or empty");

            foreach(var name in _playerNames)
            {
                _playersScores.Add(name, 0);
            }
        }

        public void InitializeGameWords()
        {
            if(_gameWords == null || _gameWords.Count < 1)
                throw new ArgumentException("Word's count cannot be lower than 1");

            var allSameLenght = !_gameWords.Any(word => word.Length != _gameWords[0].Length);

            if(!allSameLenght)
                throw new ArgumentException("All words should be same lenght");

            foreach(var word in _gameWords)
            {
                if(!_vocabularyChecker.Exists(word))
                    throw new ArgumentException("Word: {0} not allowed in this game", word);
            }
        }

        internal void InitializeGameStrings()
        {
            Random random = new Random();

            foreach(var word in _gameWords)
            {
                var wordLength = word.Length;
                var unhiddenIndex = random.Next(0, wordLength - 1); 
                var gameStringBuilder = new StringBuilder(new String('*', wordLength - 1));
                gameStringBuilder = gameStringBuilder.Insert(unhiddenIndex, word[unhiddenIndex]);
                _gameStrings.Add(word, gameStringBuilder.ToString());
            }
        }

        public IList<string> GetGameStrings()
        {
            return _gameStrings.Values.ToList();
        }

        public int SubmitGuess(string playerName, string submission)
        {
            if(String.IsNullOrEmpty(playerName) || !_playerNames.Contains(playerName))
                throw new ArgumentException("Player name empty or player does not participate in the game");

            if (String.IsNullOrEmpty(submission))
                throw new ArgumentException("Player's submission cannot be empty");

            var score = GetMatchesScore(FindMatches(submission),submission);

            UpdateScores(playerName, score);

            return (int)score;
        }

        private Dictionary<string,uint> FindMatches(string matchingSubstring)
        {
            var matches = new Dictionary<string, uint>();

            foreach(var gameStringKey in _gameStrings.Keys)
            {
                var index = gameStringKey.IndexOf(matchingSubstring);

                if (index != -1)
                {
                    matches.Add(gameStringKey, (uint)index);
                }
            }

            return matches;
        }

        private uint GetMatchesScore(Dictionary<string, uint> matchedWords, string matchingSubstring)
        {
            uint score = 0;

            foreach(var word in matchedWords.Keys)
            {
                var indexOfMatch = _gameStrings[word];
                var lenghtOfMatch = matchingSubstring.Length;
                var index = word.IndexOf(matchingSubstring);

                if (index != -1)
                {
                    score += GetMatchScore(matchingSubstring, _gameStrings[word], index);

                    // update/rewrite gameStrings dictionary
                    UpdateGameStrings(matchingSubstring, word, index);
                }
            }

            return score;
        }

        private uint GetMatchScore(string matchingSubstring, string gameString, int index)
        {
            var stringToCountScore = gameString.Substring(index, matchingSubstring.Length);
            var score = (uint)stringToCountScore.Count(ch => ch == '*');
            return score;
        }

        private void UpdateGameStrings(string matchingSubstring, string word, int index)
        {
            _gameStrings[word] = _gameStrings[word].Replace(_gameStrings[word].Substring(index, matchingSubstring.Length), matchingSubstring);
        }

        private void UpdateScores(string playerName, uint score)
        {
            _playersScores[playerName] += score;
        }
    }
}
