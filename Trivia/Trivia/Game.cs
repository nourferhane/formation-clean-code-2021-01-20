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
        private const int NUMBER_OF_PLAYER = 6;
        private readonly int[] _places = new int[NUMBER_OF_PLAYER];
        private readonly int[] _purses = new int[NUMBER_OF_PLAYER];
        private readonly bool[] _isInPenaltyBox = new bool[NUMBER_OF_PLAYER];
        private readonly List<string> _players = new List<string>();
        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportQuestions = new LinkedList<string>();
        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;
        public LinkedList<string> _rockQuestions = new LinkedList<string>();

        public Game()
        {
            for (var i = 0; i < 50; i++)
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast(("Science Question " + i));
                _sportQuestions.AddLast(("Sports Question " + i));
                _rockQuestions.AddLast(CreateRockQuestion(i));
            }
        }

        public string CreateRockQuestion(int index)
        {
            return "Rock Question " + index;
        }

        public bool AddPlayer(string playerName)
        {
            _players.Add(playerName);
            _places[GetNumberOfPlayers()] = 0;
            _purses[GetNumberOfPlayers()] = 0;
            _isInPenaltyBox[GetNumberOfPlayers()] = false;

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
            Console.WriteLine(_players[_currentPlayer] + " is the current player"); Console.WriteLine("They have rolled a " + roll);


            if (_isInPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {
                    //User is getting out of penalty box
                    _isGettingOutOfPenaltyBox = true;
                    //Write that user is getting out
                    Console.WriteLine(_players[_currentPlayer] + " is getting out of the penalty box");
                    // add roll to place
                    _places[_currentPlayer] = _places[_currentPlayer] + roll;
                    if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

                    Console.WriteLine(_players[_currentPlayer]
                            + "'s new location is "
                            + _places[_currentPlayer]);
                    Console.WriteLine("The category is " + CurrentCategory());
                    AskQuestion();
                }
                else
                {
                    Console.WriteLine(_players[_currentPlayer] + " is not getting out of the penalty box");
                    _isGettingOutOfPenaltyBox = false;
                }
            }
            else
            {
                _places[_currentPlayer] = _places[_currentPlayer] + roll;
                if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - 12;

                Console.WriteLine(_players[_currentPlayer]
                        + "'s new location is "
                        + _places[_currentPlayer]);
                Console.WriteLine("The category is " + CurrentCategory());
                AskQuestion();
            }
        }

        /// <summary>
        /// To call when the answer is right
        /// </summary>
        /// <returns></returns>
        public bool WasCorrectlyAnswered()
        {
            if (_isInPenaltyBox[_currentPlayer])
            {
                if (_isGettingOutOfPenaltyBox)
                {
                    Console.WriteLine("Answer was correct!!!!");
                    _purses[_currentPlayer]++;
                    Console.WriteLine(_players[_currentPlayer]
                            + " now has "
                            + _purses[_currentPlayer]
                            + " Gold Coins.");

                    var winner1 = !(_purses[_currentPlayer] == 6);
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;

                    return winner1;
                }

                _currentPlayer++;
                if (_currentPlayer == _players.Count) _currentPlayer = 0;
                return true;

            }

            Console.WriteLine("Answer was corrent!!!!");
            _purses[_currentPlayer]++;
            Console.WriteLine(_players[_currentPlayer]
                    + " now has "
                    + _purses[_currentPlayer]
                    + " Gold Coins.");

            var winner2 = !(_purses[_currentPlayer] == 6);
            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;

            return winner2;
        }

        /// <summary>
        /// To call when the answer is right
        /// </summary>
        /// <returns></returns>
        public bool WrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players[_currentPlayer] + " was sent to the penalty box");
            _isInPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
            //Must alwys return false 
            return true;
        }

        private void AskQuestion()
        {
            if (CurrentCategory() == "Pop")
            {
                Console.WriteLine(_popQuestions.First());
                _popQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Science")
            {
                Console.WriteLine(_scienceQuestions.First());
                _scienceQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Sports")
            {
                Console.WriteLine(_sportQuestions.First());
                _sportQuestions.RemoveFirst();
            }
            if (CurrentCategory() == "Rock")
            {
                Console.WriteLine(_rockQuestions.First());
                _rockQuestions.RemoveFirst();
            }
        }

        private string CurrentCategory()
        {
            if (_places[_currentPlayer] == 0) return "Pop";
            if (_places[_currentPlayer] == 4) return "Pop";
            if (_places[_currentPlayer] == 8) return "Pop";
            if (_places[_currentPlayer] == 1) return "Science";
            if (_places[_currentPlayer] == 5) return "Science";
            if (_places[_currentPlayer] == 9) return "Science";
            if (_places[_currentPlayer] == 2) return "Sports";
            if (_places[_currentPlayer] == 6) return "Sports";
            if (_places[_currentPlayer] == 10) return "Sports";
            return "Rock";
        }
    }

}
