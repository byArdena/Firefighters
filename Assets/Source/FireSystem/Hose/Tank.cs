using System;

namespace FireSystem
{
    public class Tank
    {
        private readonly int _maxCapacity;
        private readonly int _minCapacity;
        
        private int _currentCapacity;
        private LiquidType _liquid;

        public Tank(int maxCapacity, int minCapacity)
        {
            _maxCapacity = maxCapacity;
            _minCapacity = minCapacity;
            _currentCapacity = _minCapacity;
        }
        
        public event Action Emptied;
        public event Action Filled;

        public void ThrowOut()
        {
            _currentCapacity--;
            
            if (_currentCapacity <= _minCapacity)
                Emptied?.Invoke();
        }

        public void FillUp(LiquidType liquid)
        {
            if (liquid == LiquidType.None || (liquid != _liquid && _liquid != LiquidType.None))
                return;

            _liquid = liquid;
            _currentCapacity = _maxCapacity;
            Filled?.Invoke();
        }

        public void EmptyOut()
        {
            _currentCapacity = 0;
            _liquid = LiquidType.None;
            Emptied?.Invoke();
        }
    }
}
