using UnityEngine;

namespace Sounds
{
    [RequireComponent(typeof(Collider))]
    public abstract class SoundZone : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnvironmentSound sound) == false)
                return;
            
            OnAmbientEnter(sound);
        }

        protected abstract void OnAmbientEnter(EnvironmentSound sound);
    }
}