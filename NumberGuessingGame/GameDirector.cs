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
        public const Int32 MIN_ANSWER = 1;
        public const Int32 MAX_ANSWER = 10;
        public const Int32 STARTING_POINTS = 16;
        public Int32 HighScore { get; private set; }
        public Int32 CurrentScore { get; private set; }
        public Int32 LastScore { get; private set; }
        public Boolean KeepGuessing { get; private set; }

        private Int32 _answer;

        public Int32 MinAnswer
        {
            get
            {
                return MIN_ANSWER;
            }
            private set { }
        }

        public Int32 MaxAnswer
        {
            get
            {
                return MAX_ANSWER;
            }
            private set { }
        }

        public Int32 StartingPoints
        {
            get
            {
                return STARTING_POINTS;
            }
            private set { }
        }

        public GameDirector()
        {
            ReadGameData();
            InitializeGame();
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

        private void InitializeGame()
        {
            Random rnd = new Random();
            _answer = rnd.Next(MIN_ANSWER, MAX_ANSWER);
            CurrentScore = STARTING_POINTS;
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
