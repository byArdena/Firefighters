using System;
using FireSystem.Electricity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FireSystem
{
    [RequireComponent(typeof(ParticlePlayer))]
    [RequireComponent(typeof(AudioSource))]
    [RequireComponent(typeof(Burner))]
    public class FireSetup : MonoBehaviour
    {
        [Header("Main Options")]
        [SerializeField] private float _maxHealth;
        [SerializeField] private float _minHealth;
        [SerializeField] private float _flareStep;
        [SerializeField] private float _extinguishStep;

        [Space, Header("Electricity")] 
        [SerializeField] private ElectricityPoint _electricity;
        
        [Space, Header("ParticlePlayer")]
        [SerializeField] private float _delay;
        
        [Space, Header("Smoke")]
        [SerializeField] private ParticleSystem _smoke ;
        
        private FireCollisionDetector _collisionDetector;
        private ParticleSystem _particle;
        private ParticlePlayer _particlePlayer;
        private AudioSource _sound;
        private Burner _burner;

        private Fire _model;
        private FirePresenter _presenter;

        public FireSetup Initialize(FireCollisionDetector detector, Action<ParticlePlayer> onBurned)
        {
            _collisionDetector = detector;
            _particle = detector.GetComponent<ParticleSystem>();
            _particlePlayer = GetComponent<ParticlePlayer>();
            _sound = GetComponent<AudioSource>();
            _burner = GetComponent<Burner>();
            
            _model = new Fire(Random.Range(_maxHealth, _maxHealth * (float)ValueConstants.Three), _minHealth, _flareStep, _extinguishStep);
            _presenter = new FirePresenter(_model, _collisionDetector, _particlePlayer, _burner, onBurned);
            
            _particlePlayer.Initialize(_particle, _sound, _delay);
            _collisionDetector.Initialize(_electricity == true ? _electricity : new NormalPolicy());
            _burner.Initialize(_smoke);
            
            _presenter.Enable();
            return this;
        }

        private void OnDestroy()
        {
            _presenter?.Disable();
        }
    }
}