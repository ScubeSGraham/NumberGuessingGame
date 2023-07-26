using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberGuessingGame
{
    public class GameDirector
    {
        public const String GAME_DATA_FILE = @"C:\NumberGuessingGame\History.txt";

        public Int32 HighScore { get; private set; }
        public Int32 CurrentScore { get; private set; }
        public Int32 LastScore { get; private set; }
        public Boolean KeepGuessing { get; private set; }
        public Int32 MinAnswer { get; private set; }
        public Int32 MaxAnswer { get; private set; }
        public Int32 StartingPoints { get; private set; }


        private Int32 _answer;

        private GameDirector()
        {
            //No implementation
        }

        public GameDirector(int minAnswer, int maxAnswer, int startingPoints)
        { 
            ReadGameData();
            InitializeGame(minAnswer, maxAnswer, startingPoints);
        }

        private void ReadGameData()
        {
            using (StreamReader file = new StreamReader(GAME_DATA_FILE))
            {
                HighScore = int.Parse(file.ReadLine());
                LastScore = int.Parse(file.ReadLine());
                file.Close();
            }
        }

        private void SaveGameData()
        {
            using (StreamWriter writer = new StreamWriter(GAME_DATA_FILE))
            {
                int outputScore = HighScore;

                if (CurrentScore > HighScore)
                {
                    outputScore = CurrentScore;
                }

                writer.WriteLine(outputScore);
                writer.WriteLine(CurrentScore);
            }
        }

        private void InitializeGame(int minAnswer, int maxAnswer, int startingPoints)
        {
            MinAnswer = minAnswer;
            MaxAnswer = maxAnswer;
            StartingPoints = startingPoints;
            Random rnd = new Random();
            _answer = rnd.Next(MinAnswer, MaxAnswer);
            CurrentScore = StartingPoints;
            KeepGuessing = true;
        }

        public ResultType GuessNumber(Int32 guess)
        {
            if (!KeepGuessing)
            {
                throw new InvalidOperationException("Sorry, the game is over");
            }

            ResultType result = ResultType.Initialized;

            if (guess == _answer)
            {
                KeepGuessing = false;
                result = ResultType.Correct;
                SaveGameData();
                return result;
            }
            else if (guess > _answer)
            {
                result = ResultType.TooHigh;
            }
            else if (guess < _answer)
            {
                result = ResultType.TooLow;
            }

            CurrentScore = CurrentScore / 2;

            if (CurrentScore == 0)
            {
                KeepGuessing = false;
                result = ResultType.LoseGame;
                SaveGameData();
            }

            return result;
        }
    }
}
