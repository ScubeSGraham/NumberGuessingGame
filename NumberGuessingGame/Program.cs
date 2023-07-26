// See https://aka.ms/new-console-template for more information

int highScore = 0;
int lastScore = 0;

using (StreamReader file = new StreamReader(@"C:\NumberGuessingGame\History.txt"))
{
    highScore = int.Parse(file.ReadLine());
    lastScore = int.Parse(file.ReadLine());
    file.Close();
}

Random rnd = new Random();
int answer = rnd.Next(1, 11);
int currentPoints = 16;

Console.WriteLine("I'm thinking of a number between 1 and 10, what is it?");
Console.WriteLine("Your highest score ever is: " + highScore);
Console.WriteLine("Last time, you got: " + lastScore);

bool guessAgain = true;

while (guessAgain == true)
{
    Console.WriteLine("Current available points: " + currentPoints);

    String? inputString = Console.ReadLine();
    int inputNumber = int.Parse(inputString);

    if (inputNumber == answer)
    {
        Console.WriteLine("That's right!  Good job!!");
        Console.WriteLine("You received " + currentPoints + " points.");
        guessAgain = false;
    }
    else if (inputNumber > answer)
    {
        Console.WriteLine("That's too high, guess something lower");
    }
    else if (inputNumber < answer)
    {
        Console.WriteLine("That's too low, guess something higher");
    }

    if (inputNumber != answer)
    {
        currentPoints = currentPoints / 2;

        if (currentPoints == 0)
        {

            Console.WriteLine("Sorry, you failed, try again.");
            guessAgain = false;
        }
    }
}

using (StreamWriter writer = new StreamWriter(@"C:\NumberGuessingGame\History.txt"))
{
    int outputScore = highScore;

    if (currentPoints > highScore)
    {
        outputScore = currentPoints;
    }

    writer.WriteLine(outputScore);
    writer.WriteLine(currentPoints);
}



