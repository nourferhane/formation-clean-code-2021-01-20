using System;

namespace Parrot
{
    public class AfricanParrot : IParrot
    {
        private const double LOAD_FACTOR = 9.0;
        private const double BASE_SPEED = 12.0;
        private int _numberOfCoconuts;

        public AfricanParrot(int numberOfCoconuts)
        {
            this._numberOfCoconuts = numberOfCoconuts;
        }
        public double GetSpeed()
        {
            return Math.Max(0, BASE_SPEED - LOAD_FACTOR * _numberOfCoconuts);
        }
    }
}
