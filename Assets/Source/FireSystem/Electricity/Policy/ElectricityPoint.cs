using System.Collections;
using UnityEngine;

namespace FireSystem.Electricity
{
    public class ElectricityPoint : MonoBehaviour, IElectricityPolicy
    {
        [Header("Damageable Spark")]
        [SerializeField] private ParticleSystem _spark;
        [SerializeField] private AudioSource _source;
        
        [Space, Header("Standard")]
        [SerializeField] private ParticleSystem _particle;
        
        private bool _canInteract;
        private bool _canUseSpark;
        private float _delay;

        public void Initialize(float delay)
        {
            _delay = delay;
            _canUseSpark = true;
            _particle.Play();
        }
        
        public void SetInteraction(bool canInteract)
        {
            _canInteract = canInteract;
            
            if (_canInteract == true)
                _particle.Stop();
        }
        
        public bool CanInteract()
        {
            if (_canInteract == true || _canUseSpark == false) 
                return _canInteract;
            
            _canUseSpark = false;
            StartCoroutine(CoolDown());

            return _canInteract;
        }

        private IEnumerator CoolDown()
        {
            _spark.Play();
            _source.Play();
            yield return new WaitForSeconds(_delay);
            _canUseSpark = true;
        }
    }
}