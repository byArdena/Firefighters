using System;
using UnityEngine;

namespace HealthSystem
{
    public class HealthCollisionDetector : MonoBehaviour
    {
        private int _typeDamage;
        
        public event Action<TypeDamage> TakingDamage;
        public event Action Healing;

        public void Detect()
        {
            TakingDamage?.Invoke(TypeDamage.Fire);
        }
    }
}