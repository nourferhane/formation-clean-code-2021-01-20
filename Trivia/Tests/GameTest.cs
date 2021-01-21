using System;
using System.IO;
using ApprovalTests;
using ApprovalTests.Reporters;
using Trivia;
using Xunit;


namespace Tests
{
    [UseReporter(typeof(DiffReporter))]
    public class GameTest
    {
        [Fact]
        public void Test1()
        {
            var fakeconsole = new StringWriter();
            Console.SetOut(fakeconsole);
            var game = new Game();
            game.AddPlayer("Cedric");
            game.Play(12);
            game.ProcessWrongAnswer();
            game.Play(2);
            game.Play(13);
            game.ProcessCorrectAnswer();
            game.Play(13);
            Approvals.Verify(fakeconsole.ToString());
        }
        
        [Fact]
        public void Test2()
        {
            var fakeconsole = new StringWriter();
            Console.SetOut(fakeconsole);
            var game = new Game();
            game.AddPlayer("Cedric");
            game.AddPlayer("Eloïse");
            game.Play(1);
            game.ProcessCorrectAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            Approvals.Verify(fakeconsole.ToString());
        }
        
        [Fact]
        public void Test3()
        {
            var fakeconsole = new StringWriter();
            Console.SetOut(fakeconsole);
            var game = new Game();
            game.AddPlayer("Cedric");
            game.AddPlayer("Eloïse");
            game.Play(1);
            game.ProcessWrongAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            game.Play(2);
            game.ProcessCorrectAnswer();
            Approvals.Verify(fakeconsole.ToString());
        }

        [Fact]
        public void Can_Add_Ten_Players()
        {
            //Arrange
            var game = new Game();

            //Act
            for (int i = 0; i < 10; i++)
            {
                game.AddPlayer("Player" + i);
            }

            //Assert
            Assert.Equal(10, game.GetNumberOfPlayers());
        }
    }
}
