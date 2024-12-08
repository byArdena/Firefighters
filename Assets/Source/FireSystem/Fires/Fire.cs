using System;
using UnityEngine;

namespace FireSystem
{
    public class Fire
    {
        private readonly float _maxHealth;
        private readonly float _minHealth;
        private readonly float _flareStep;
        private readonly float _extinguishStep;
    
        private float _health;

        public event Action Extinguished;
        public event Action Burned;

        public Fire(float maxHealth, float minHealth, float flareStep, float extinguishStep)
        {
            _maxHealth = maxHealth;
            _minHealth = minHealth;
            _flareStep = flareStep;
            _extinguishStep = extinguishStep;
        }

        public void Extinguish()
        {
            _health -= _extinguishStep;

            if (_health > _minHealth)
                return;
            
            Extinguished?.Invoke();
            _health = _maxHealth;
        }

        public void FlareUp()
        {
            if (_health >= _maxHealth)
            {
                Burned?.Invoke();
                return;
            }
            
            float value = _health + _flareStep;

            _health = value > _maxHealth ? _maxHealth : value;
        }
    }
}
