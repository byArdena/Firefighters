using System;
using FireSystem.Electricity;
using UnityEngine;

namespace FireSystem
{
    public class FireCollisionDetector : MonoBehaviour
    {
        private Collider _collider;
        private bool _isAllowed;
        private IElectricityPolicy _policy;
        
        public event Action Extinguishing;

        public void Initialize(IElectricityPolicy policy)
        {
            _collider = GetComponent<Collider>();
            _policy = policy;
        }

        public void AllowDetect()
        {
            _collider.enabled = true;
            _isAllowed = true;
        }

        public void DisallowDetect()
        {
            _collider.enabled = false;
            _isAllowed = false;
        }

        private void OnParticleCollision(GameObject other)
        {
            if (_isAllowed == false)
                return;

            if (other.TryGetComponent(out Water water) == false)
                return;
            
            if (_policy.CanInteract() == false)
                return;
            
            Extinguishing?.Invoke();
        }
    }
}