using ActionBars;
using HealthSystem;
using UnityEngine;

namespace Survivors
{
    [RequireComponent(typeof(HealthSetup))]
    public class SurvivorSetup : MonoBehaviour
    {
        [SerializeField] private float _valueHealth;
        [SerializeField] HealthSetup _health;
        [SerializeField] private float _interaction;
        [SerializeField] BehaviourSurvivor _survivor;

        public void Initialize(ActionSlider slider, Transform player)
        {
            _health.Initialize(new Damage(), _valueHealth);
            _survivor.Initialize(player, slider, _interaction);
        }
    }
}