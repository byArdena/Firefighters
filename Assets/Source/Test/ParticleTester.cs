using UnityEngine;

namespace Test
{
    public class ParticleTester : MonoBehaviour
    {
        private void OnParticleCollision(GameObject other)
        {
            print(other.name);
        }
    }
}