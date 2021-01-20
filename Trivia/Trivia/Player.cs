namespace Trivia
{
    public class Player
    {
        private const int NUMBER_OF_PLACES_IN_BOARD = 12;

        public string Name { get; set; }

        public int Purse { get; private set; }

        public bool IsInPenaltyBox { get; set; }

        public int Place { get; set; }

        internal void EarnCoin()
        {
            Purse++;
        }

        internal void Move(int roll)
        {
            Place += roll;
            if (Place > 11)
            {
                Place -= NUMBER_OF_PLACES_IN_BOARD;
            }
        }
    }
}
