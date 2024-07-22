// See https://aka.ms/new-console-template for more information
using MultiGuess;

Console.WriteLine("Hello, World!");

var vocabularyChecker = new VocabularyChecker();
var game = new MultiplayerGuessingGame(new List<string> {"bob", "steev"}, new List<string> {"abaci", "aback", "abamp", "abase" }, vocabularyChecker);

game.SubmitGuess("bob", "bamp");

Console.ReadLine();