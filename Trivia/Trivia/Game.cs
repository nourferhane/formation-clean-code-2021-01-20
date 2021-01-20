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
        private const int MAX_QUESTIONS_BY_CATEGORY = 50;
        private const int COINS_NEEDED_TO_WIN = 6;
        private const int NUMBER_OF_PLACES_IN_BOARD = 12;
        private readonly int[] _places = new int[NUMBER_OF_PLAYER];
        private readonly int[] _purses = new int[NUMBER_OF_PLAYER];
        private readonly bool[] _isInPenaltyBox = new bool[NUMBER_OF_PLAYER];
        private readonly List<string> _players = new List<string>();
        private readonly LinkedList<string> _popQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _scienceQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _sportQuestions = new LinkedList<string>();
        private readonly LinkedList<string> _rockQuestions = new LinkedList<string>();
        private int _currentPlayer;
        private bool _isGettingOutOfPenaltyBox;        

        public Game()
        {
            for (var i = 0; i < MAX_QUESTIONS_BY_CATEGORY; i++)
            {
                _popQuestions.AddLast("Pop Question " + i);
                _scienceQuestions.AddLast(("Science Question " + i));
                _sportQuestions.AddLast(("Sports Question " + i));
                _rockQuestions.AddLast("Rock Question " + i);
            }
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
            Console.WriteLine(_players[_currentPlayer] + " is the current player");
            Console.WriteLine("They have rolled a " + roll);

            if (_isInPenaltyBox[_currentPlayer])
            {
                if (roll % 2 != 0)
                {                    
                    _isGettingOutOfPenaltyBox = true;                    
                    Console.WriteLine(_players[_currentPlayer] + " is getting out of the penalty box");                   
                    _places[_currentPlayer] = _places[_currentPlayer] + roll;
                    if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - NUMBER_OF_PLACES_IN_BOARD;

                    Console.WriteLine(_players[_currentPlayer]
                            + "'s new location is "
                            + _places[_currentPlayer]);
                    Console.WriteLine("The category is " + GetCurrentQuestionCategory());
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
                if (_places[_currentPlayer] > 11) _places[_currentPlayer] = _places[_currentPlayer] - NUMBER_OF_PLACES_IN_BOARD;

                Console.WriteLine(_players[_currentPlayer]
                        + "'s new location is "
                        + _places[_currentPlayer]);
                Console.WriteLine("The category is " + GetCurrentQuestionCategory());
                AskQuestion();
            }
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
                    Console.WriteLine(_players[_currentPlayer]
                            + " now has "
                            + _purses[_currentPlayer]
                            + " Gold Coins.");

                    var doesGameContinue = _purses[_currentPlayer] != COINS_NEEDED_TO_WIN;
                    _currentPlayer++;
                    if (_currentPlayer == _players.Count) _currentPlayer = 0;

                    return doesGameContinue;
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

            var doesGameContinue2 = _purses[_currentPlayer] != COINS_NEEDED_TO_WIN;
            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;

            return doesGameContinue2;
        }

        /// <summary>
        /// To call when the answer is Wrong
        /// </summary>
        /// <returns>always return true because game continues</returns>
        public bool ProcessWrongAnswer()
        {
            Console.WriteLine("Question was incorrectly answered");
            Console.WriteLine(_players[_currentPlayer] + " was sent to the penalty box");
            _isInPenaltyBox[_currentPlayer] = true;

            _currentPlayer++;
            if (_currentPlayer == _players.Count) _currentPlayer = 0;
             
            return true;
        }

        private void AskQuestion()
        {
            switch (GetCurrentQuestionCategory())
            {
                case "Pop":
                    Console.WriteLine(_popQuestions.First());
                    _popQuestions.RemoveFirst();
                    break;
                case "Science":
                    Console.WriteLine(_scienceQuestions.First());
                    _scienceQuestions.RemoveFirst();
                    break;
                case "Sports":
                    Console.WriteLine(_sportQuestions.First());
                    _sportQuestions.RemoveFirst();
                    break;
                case "Rock":
                    Console.WriteLine(_rockQuestions.First());
                    _rockQuestions.RemoveFirst();
                    break;
            }
        }

        private string GetCurrentQuestionCategory()
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
