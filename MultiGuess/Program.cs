// See https://aka.ms/new-console-template for more information
using MultiGuess;

Console.WriteLine("Hello, World!");

var vocabularyChecker = new VocabularyChecker();
var game = new MultiplayerGuessingGame(new List<string> {"bob", "steev"}, new List<string> {"abaci", "aback", "abamp", "abase" }, vocabularyChecker);


foreach (var gameString in game.GetGameStrings())
    Console.WriteLine(gameString);

game.SubmitGuess("bob", "bamp");
Console.WriteLine("Guessing: bamp");

foreach (var gameString in game.GetGameStrings())
    Console.WriteLine(gameString);

Console.ReadLine();