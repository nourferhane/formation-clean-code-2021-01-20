using System;

namespace Parrot
{
    public class NorwegianBlueParrot : ICanGiveSpeed
    {
        private bool _isNailed;
        private readonly double _voltage;
        public NorwegianBlueParrot(bool isNailed,  double voltage)
        {
            _isNailed = isNailed;
            _voltage = voltage;
        }

        public double GetSpeed()
        {
            return _isNailed ? 0 : GetBaseSpeed(_voltage);
        }

        private double GetBaseSpeed(double voltage)
        {
            return Math.Min(24.0, voltage * GetBaseSpeed());
        }

        private double GetBaseSpeed()
        {
            return 12.0;
        }
    }
}
