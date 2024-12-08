using System.Collections.Generic;
using TaskBars;
using UnityEngine;

namespace HealthSystem
{
    [RequireComponent(typeof(HealthCollisionDetector))]
    [RequireComponent(typeof(Screamer))]
    public class HealthSetup : MonoBehaviour
    {
        [Header("Main Options")]
        [SerializeField] private float _minHealth;
        [SerializeField] private float _stepHeal;
        [SerializeField] private SliderTask _task;
        [SerializeField] private AudioSource _source;

        [Space, Header("Types Of Damage")] 
        [SerializeField] private List<SerializedPair<TypeDamage, float>> _damages;

        private HealthCollisionDetector _healthDetector;
        private Health _model;
        private HealthPresenter _presenter;
        private Screamer _screamer;

        public HealthSetup Initialize(IDamageable damageable, float health)
        {
            _healthDetector = GetComponent<HealthCollisionDetector>();
            _screamer = GetComponent<Screamer>();

            _model = new Health(damageable, _minHealth, health, _stepHeal, _damages);
            _presenter = new HealthPresenter(_model, _healthDetector, _task, _screamer);
            _task.Report(Mathf.CeilToInt(health), Mathf.CeilToInt(health));
            _screamer.Initialize(_source);
            
            _presenter.Enable();
            return this;
        }

        private void OnDestroy()
        {
            _presenter.Disable();
        }
    }
}