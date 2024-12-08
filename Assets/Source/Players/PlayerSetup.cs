using System.Collections.Generic;
using ActionBars;
using Saves;
using Sounds;
using UnityEngine;

namespace Players
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(NormalCollisionDetector))]
    [RequireComponent(typeof(PlayerOverview))]
    [RequireComponent(typeof(EnvironmentSound))]
    [RequireComponent(typeof(ActionRadar))]
    public class PlayerSetup : MonoBehaviour
    {
        [SerializeField] private ParticlePlayer _hose;
        [SerializeField] private Joystick _joystickRotator;

        [Space, Header("Actions")] 
        [SerializeField] private ActionButton _button;
        [SerializeField] private float _interactionDistance;

        [Space, Header("Rotation Option")]
        [SerializeField] private Transform _cameraPoint;
        [SerializeField] private float _speedCamera;
        [SerializeField] private float _minAngle;
        [SerializeField] private float _maxAngle;

        [Space, Header("Sound")] 
        [SerializeField] private AudioSource[] _walks;
        [SerializeField] private AudioSource[] _ambient;
        
        private PlayerMovement _playerMovement;
        private NormalCollisionDetector _normalCollisionDetector;
        private PlayerOverview _playerOverview;
        private EnvironmentSound _sound;
        private ActionRadar _radar;

        private PlayerInput _playerInput;
        private InputPresenter _inputPresenter;

        private Movement _movement;
        private MovementPresenter _movementPresenter;

        public void Initialize(IReadOnlyCharacteristics characteristics, List<IAction> actions)
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _normalCollisionDetector = GetComponent<NormalCollisionDetector>();
            _playerOverview = GetComponent<PlayerOverview>();
            _sound = GetComponent<EnvironmentSound>();
            _radar = GetComponent<ActionRadar>();

            _playerInput = new PlayerInput();
            _inputPresenter = new InputPresenter(_playerInput, _hose, _playerMovement, _playerOverview, _joystickRotator, _radar);
            _movement = new Movement(characteristics.Speed, _speedCamera, _minAngle, _maxAngle, _cameraPoint.eulerAngles);
            _movementPresenter =
                new MovementPresenter(_movement, _playerMovement, _playerOverview, _normalCollisionDetector, _sound);
            
            _sound.Initialize(_walks, _ambient);
            _playerOverview.Initialize(_cameraPoint);
            _playerMovement.Initialize();
            _radar.Initialize(actions, transform, _button, _interactionDistance);
            
            _inputPresenter.Enable();
            _movementPresenter.Enable();
        }

        private void OnDestroy()
        {
            _inputPresenter.Disable();
            _movementPresenter.Disable();
        }
    }
}