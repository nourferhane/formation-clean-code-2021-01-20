using System;
using System.Collections.Generic;

namespace Trivia
{
    /// <summary>
    /// The Game
    /// </summary>
    public class Game
    {
        private const int MAX_QUESTIONS_BY_CATEGORY = 50;
        private const int COINS_NEEDED_TO_WIN = 6;
        private readonly List<Player> _players = new List<Player>();
        private readonly Queue<string> _popQuestions = new Queue<string>();
        private readonly Queue<string> _scienceQuestions = new Queue<string>();
        private readonly Queue<string> _sportQuestions = new Queue<string>();
        private readonly Queue<string> _rockQuestions = new Queue<string>();
        private int _currentPlayer;

        public Game()
        {
            for (var i = 0; i < MAX_QUESTIONS_BY_CATEGORY; i++)
            {
                _popQuestions.Enqueue("Pop Question " + i);
                _scienceQuestions.Enqueue(("Science Question " + i));
                _sportQuestions.Enqueue(("Sports Question " + i));
                _rockQuestions.Enqueue("Rock Question " + i);
            }
        }

        public bool AddPlayer(string playerName)
        {
            _players.Add(new Player { Name = playerName });

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
            return true;
        }

        public int GetNumberOfPlayers() => _players.Count;

        public void Play(int roll)
        {
            Console.WriteLine(GetPlayerName() + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (ManagePlayerPenalty(roll))
            {
                Move(roll);
                AskQuestion();
            }
        }

        private void Move(int roll)
        {
            GetCurrentPlayer().Move(roll);
            Console.WriteLine(GetPlayerName()
                + "'s new location is "
                + GetCurrentPlayer().Place);
        }

        private bool ManagePlayerPenalty(int roll)
        {
            if (GetCurrentPlayer().IsInPenaltyBox)
            {
                if (roll % 2 != 0)
                {
                    GetCurrentPlayer().IsInPenaltyBox = false;
                    Console.WriteLine(GetPlayerName() + " is getting out of the penalty box");
                    return true;
                }

                Console.WriteLine(GetPlayerName() + " is not getting out of the penalty box");
                return false;
            }
            return true;
        }

        private string GetPlayerName() => GetCurrentPlayer().Name;

        private Player GetCurrentPlayer() => _players[_currentPlayer];

        /// <summary>
        /// To call when the answer is right
        /// </summary>
        /// <returns>if true, the game should continue</returns>
        public bool ProcessCorrectAnswer()
        {
            if (GetCurrentPlayer().IsInPenaltyBox)
            {
                _currentPlayer++;
                if (_currentPlayer == _players.Count)
                {
                    _currentPlayer = 0;
                }
                return true;
            }

            Console.WriteLine("Answer was correct!!!!");
            GetCurrentPlayer().EarnCoin();
            Console.WriteLine(GetPlayerName()
                    + " now has "
                    + GetCurrentPlayer().Purse
                    + " Gold Coins.");

            var doesGameContinue2 = GetCurrentPlayer().Purse != COINS_NEEDED_TO_WIN;
            _currentPlayer++;
            if (_currentPlayer == _players.Count)
            {
                _currentPlayer = 0;
            }

            return doesGameContinue2;
        }

        /// <summary>
        /// To call when the answer is Wrong
        /// </summary>
        /// <returns>always return true because game continues</returns>
        public bool ProcessWrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(GetPlayerName() + " was sent to the penalty box");
            GetCurrentPlayer().IsInPenaltyBox = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count)
            {
                _currentPlayer = 0;
            }

            return true;
        }

        private void AskQuestion()
        {
            Console.WriteLine("The category is " + GetCurrentQuestionCategory());
            var currentQueue = (GetCurrentQuestionCategory()) switch
            {
                "Pop" => _popQuestions,
                "Science" => _scienceQuestions,
                "Sports" => _sportQuestions,
                _ => _rockQuestions
            };

            Console.WriteLine(currentQueue.Dequeue());
        }

        private string GetCurrentQuestionCategory()
        {
            return (GetCurrentPlayer().Place % 4) switch
            {
                0 => "Pop",
                1 => "Science",
                2 => "Sports",
                _ => "Rock"
            };
        }
    }
}
