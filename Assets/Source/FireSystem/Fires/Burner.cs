using UnityEngine;

namespace FireSystem
{
    public class Burner : MonoBehaviour
    {
        private ParticleSystem _particle;
        private Transform _transform;
        
        public void Initialize(ParticleSystem particle)
        {
            _particle = particle;
            _transform = transform;
        }

        public void Burn()
        {
            Instantiate(_particle, _transform.position, Quaternion.identity);
        }
    }
}