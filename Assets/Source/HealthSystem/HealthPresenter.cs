using TaskBars;
using UnityEngine;

namespace HealthSystem
{
    public class HealthPresenter
    {
        private readonly Health Model;
        private readonly HealthCollisionDetector HealthDetector;
        private readonly IProgress Task;
        private readonly Screamer Screamer;

        public HealthPresenter(Health model, HealthCollisionDetector healthDetector, IProgress task, Screamer screamer)
        {
            Model = model;
            HealthDetector = healthDetector;
            Task = task;
            Screamer = screamer;
        }

        public void Enable()
        {
            Model.Dying += OnDying;
            Model.DamageTaken += OnDamageTaken;
            
            HealthDetector.TakingDamage += OnTakingDamage;
            HealthDetector.Healing += OnHealing;
        }

        public void Disable()
        {
            Model.Dying -= OnDying;
            Model.DamageTaken -= OnDamageTaken;
            
            HealthDetector.TakingDamage -= OnTakingDamage;
            HealthDetector.Healing -= OnHealing;
        }

        private void OnDying()
        {
            Task.Lose();
        }

        private void OnDamageTaken(float health, float maxHealth)
        {
            Screamer.Play();
            Task.Report(Mathf.CeilToInt(health), Mathf.CeilToInt(maxHealth));
        }

        private void OnTakingDamage(TypeDamage typeDamage)
        {
            Model.TakeDamage(typeDamage);
        }

        private void OnHealing()
        {
            Model.Heal();
        }
    }
}