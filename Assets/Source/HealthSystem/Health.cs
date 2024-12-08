using System;
using System.Collections.Generic;
using UnityEngine;

namespace HealthSystem
{
    public class Health: IDead
    {
        private readonly float _minHealth;
        private readonly float _maxHealth;
        private readonly float _stepHeal;
        private readonly List<SerializedPair<TypeDamage, float>> _damages;
        private readonly IDamageable _damageable;
        
        private float _currentHealth;
        
        public event Action Dying;
        public event Action<float, float> DamageTaken;
        
        public Health(IDamageable damageable, float minHealth, float maxHealth, float stepHeal, List<SerializedPair<TypeDamage, float>> damages)
        {
            _damageable = damageable;
            _minHealth = minHealth;
            _maxHealth = maxHealth;
            _stepHeal = stepHeal;
            _damages = damages;
            _currentHealth = maxHealth;
        }

        public void TakeDamage(TypeDamage typeDamage)
        {
            if (_currentHealth <= _minHealth)
                return;
            
            _currentHealth -= DetermineDamage(typeDamage);
            _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);
            DamageTaken?.Invoke(_currentHealth, _maxHealth);

            if (_currentHealth <= _minHealth)
            {
                Dying?.Invoke();
            }
        }

        public void Heal()
        {
            _currentHealth += _stepHeal;
            _currentHealth = Mathf.Clamp(_currentHealth, _minHealth, _maxHealth);
        }

        private float DetermineDamage(TypeDamage typeDamage)
        {
            return _damageable.CalculateDamage(_damages.Find(item=>item.Key == typeDamage).Value);
        }
    }
}