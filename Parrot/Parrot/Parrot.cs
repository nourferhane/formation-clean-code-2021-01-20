using System;

namespace Parrot
{
    public class Parrot : IParrot
    {
        private readonly bool _isNailed;
        private readonly ParrotTypeEnum _type;
        private readonly double _voltage;

        public Parrot(ParrotTypeEnum type, double voltage, bool isNailed)
        {
            _type = type;
            _voltage = voltage;
            _isNailed = isNailed;
        }

        public double GetSpeed()
        {
            switch (_type)
            {
                case ParrotTypeEnum.NORWEGIAN_BLUE:
                    return _isNailed ? 0 : GetBaseSpeed(_voltage);
            }

            throw new Exception("Should be unreachable");
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