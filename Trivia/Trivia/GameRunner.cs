using System;

namespace Trivia
{
    public class GameRunner
    {
        public static void Main(string[] args)
        {
            var aGame = new Game();

            aGame.AddPlayer("Chet");
            aGame.AddPlayer("Pat");
            aGame.AddPlayer("Sue");

            var rand = new Random();

            bool _doesGameContinue;
            do
            {
                aGame.Play(rand.Next(5) + 1);

                if (rand.Next(9) == 7)
                {
                    _doesGameContinue = aGame.ProcessCorrectAnswer();
                }
                else
                {
                    _doesGameContinue = aGame.ProcessWrongAnswer();
                }
            } while (_doesGameContinue);
        }
    }
}