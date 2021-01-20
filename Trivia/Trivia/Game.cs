using System;
using System.Collections.Generic;
using System.Linq;

namespace Trivia
{
    /// <summary>
    /// The Game
    /// </summary>
    public class Game
    {
        private const int MAX_QUESTIONS_BY_CATEGORY = 50;
        private const int COINS_NEEDED_TO_WIN = 6;
        private const int NUMBER_OF_PLACES_IN_BOARD = 12;
        private readonly List<int> _places = new List<int>();
        private readonly List<int> _purses = new List<int>();
        private readonly List<bool> _isInPenaltyBox = new List<bool>();
        private readonly List<string> _players = new List<string>();
        private readonly Queue<string> _popQuestions = new Queue<string>();
        private readonly Queue<string> _scienceQuestions = new Queue<string>();
        private readonly Queue<string> _sportQuestions = new Queue<string>();
        private readonly Queue<string> _rockQuestions = new Queue<string>();
        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;

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
            _players.Add(playerName);
            _places.Add(0);
            _purses.Add(0);
            _isInPenaltyBox.Add(false);

            Console.WriteLine(playerName + " was added");
            Console.WriteLine("They are player number " + _players.Count);
            return true;
        }

        public int GetNumberOfPlayers()
        {
            return _players.Count;
        }

        public void Roll(int roll)
        {
            Console.WriteLine(GetPlayerName() + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_isInPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    _isGettingOutOfPenaltyBox = true;
                    Console.WriteLine(GetPlayerName() + " is getting out of the penalty box");
                    _places[_currentPlayer] = _places[_currentPlayer] + roll;
                    if (_places[_currentPlayer] > 11)
                    {
                        _places[_currentPlayer] = _places[_currentPlayer] - NUMBER_OF_PLACES_IN_BOARD;
                    }

                    Console.WriteLine(GetPlayerName()
                            + "'s new location is "
                            + _places[_currentPlayer]);
                    Console.WriteLine("The category is " + GetCurrentQuestionCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(GetPlayerName() + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                _places[_currentPlayer] = _places[_currentPlayer] + roll;
                if (_places[_currentPlayer] > 11)
                {
                    _places[_currentPlayer] = _places[_currentPlayer] - NUMBER_OF_PLACES_IN_BOARD;
                }

                Console.WriteLine(GetPlayerName()
                        + "'s new location is "
                        + _places[_currentPlayer]);
                Console.WriteLine("The category is " + GetCurrentQuestionCategory());
                AskQuestion();
            }
        }

        private string GetPlayerName()
        {
            return _players[_currentPlayer];
        }

        /// <summary>
        /// To call when the answer is right
        /// </summary>
        /// <returns>if true, the game should continue</returns>
        public bool ProcessCorrectAnswer()
        {
            if (_isInPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _purses[_currentPlayer]++;
                    Console.WriteLine(GetPlayerName()
                            + " now has "
                            + _purses[_currentPlayer]
                            + " Gold Coins.");

                    var doesGameContinue = _purses[_currentPlayer] != COINS_NEEDED_TO_WIN;
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count)
                    {
                        _currentPlayer = 0;
                    }

                    return doesGameContinue;
                }

                _currentPlayer++;
                if (_currentPlayer == _players.Count)
                {
                    _currentPlayer = 0;
                }
                return true;

            }

            Console.WriteLine("Answer was corrent!!!!");
            _purses[_currentPlayer]++;
            Console.WriteLine(GetPlayerName()
                    + " now has "
                    + _purses[_currentPlayer]
                    + " Gold Coins.");

            var doesGameContinue2 = _purses[_currentPlayer] != COINS_NEEDED_TO_WIN;
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
            _isInPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count)
            {
                _currentPlayer = 0;
            }

            return true;
        }

        private void AskQuestion()
        {
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
            return (_places[_currentPlayer] % 4) switch
            {
                0 => "Pop",
                1 => "Science",
                2 => "Sports",
                _ => "Rock"
            };
        }
    }

}
