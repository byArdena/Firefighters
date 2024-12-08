using System.Collections.Generic;
using CompassBars;
using TaskBars;
using UnityEngine;

namespace Survivors
{
    public class ExitArea : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private Marker _marker;
        [SerializeField] private ParticleSystem _particle;

        public void Initialize(out Marker marker)
        {
            marker = _marker;
            _particle.Play();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out ISurvivor survivor) == true)
            {
                survivor.ChangeTarget(_target);
                
                if (other.TryGetComponent(out IProgress task) == true)
                {
                    task.Win();
                }
            }
            
        }
    }
}