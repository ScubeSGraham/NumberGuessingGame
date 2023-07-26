using NumberGuessingGame;

GameDirector director = new GameDirector(1, 100, 128);

Console.WriteLine("I'm thinking of a number between " + director.MinAnswer + " and " + director.MaxAnswer + ", what is it?");
Console.WriteLine("Your highest score ever is: " + director.HighScore);
Console.WriteLine("Last time, you got: " + director.LastScore);

while (director.KeepGuessing)
{
    Console.WriteLine("Current available points: " + director.CurrentScore);

    int inputNumber = int.Parse(Console.ReadLine());
    ResultType result = director.GuessNumber(inputNumber);

    if (result == ResultType.Correct)
    {
        Console.WriteLine("That's right!  Good job!!");
        Console.WriteLine("You received " + director.CurrentScore + " points.");
    }
    else if (result == ResultType.TooHigh)
    {
        Console.WriteLine("That's too high, guess something lower");
    }
    else if (result == ResultType.TooLow)
    {
        Console.WriteLine("That's too low, guess something higher");
    }
    else if (result == ResultType.LoseGame)
    {
        Console.WriteLine("Sorry, you failed, try again.");
    }
}



