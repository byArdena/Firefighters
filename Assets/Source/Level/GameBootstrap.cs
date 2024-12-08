using System;
using System.Collections.Generic;
using System.Linq;
using ActionBars;
using CompassBars;
using FireSystem;
using HealthSystem;
using Menu;
using Players;
using Saves;
using Survivors;
using TaskBars;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Screen = Menu.Screen;

namespace Level
{
    [RequireComponent(typeof(Stopper))]
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private Game _game;
        [SerializeField] private PlayerSetup _player;
        [SerializeField] private TankSetup _tank;
        [SerializeField] private HealthSetup _health;

        [Space, Header("Spawner")] 
        [SerializeField] private Transform _spawnPoint;
        [SerializeField] private List<SerializedPair<Missions, HouseSetup[]>> _templates;

        [Space, Header(nameof(CompassBar))] 
        [SerializeField] private CompassBar _compass;
        [SerializeField] private List<Marker> _markers;

        [Space, Header(nameof(ActionBar))] 
        [SerializeField] private ActionSlider _slider;
        [SerializeField] private List<ActionBar> _actionsBars;

        [Space, Header(nameof(Game))] 
        [SerializeField] private TextMeshProUGUI _rewardWin;
        [SerializeField] private TextMeshProUGUI _rewardLose;
        [SerializeField] private Button[] _menuButtons;
        [SerializeField] private Button _pause;
        [SerializeField] private Button _release;
        [SerializeField] private Screen _screen;
        [SerializeField] private float _prepareDelay;

        [Space, Header(nameof(TaskBoard))] 
        [SerializeField] private TextView _textTemplate;
        [SerializeField] private SliderView _sliderTemplate;
        [SerializeField] private TaskBoard _taskBoard;
        [SerializeField] private List<Task> _tasks;
        [SerializeField] private RectTransform _board;
        [SerializeField] private Sprite _winIcon;
        [SerializeField] private Sprite _loseIcon;
        [SerializeField] private Task _playerTask;

        [Space, Header(nameof(ExitArea))] 
        [SerializeField] private ExitArea _exitArea;

        private Stopper _stopper;
        private HouseSetup _house;
        private SaveService _saveService;
        private Progress _model;
        private GamePresenter _presenter;
        private IFactory<HouseSetup> _factory;
        private Dictionary<Type, TaskView> _taskTemplates;

        private void Awake()
        {
            _taskTemplates = new Dictionary<Type, TaskView>
            {
                { typeof(TextView), _textTemplate },
                { typeof(SliderView), _sliderTemplate }
            };

            _factory = new HouseFactory(_templates, _spawnPoint.position, Instantiate);
            _stopper = GetComponent<Stopper>();
            _house = _factory.Create();
            _saveService = new SaveService();
            _model = new Progress(_saveService);

            _actionsBars.AddRange(_house.Initialize(_slider, _player.transform, _exitArea,
                out List<Marker> markers, out List<Task> tasks, out Action initializeCallback));
            _markers.AddRange(markers);
            _tasks.AddRange(tasks);

            _taskBoard.Initialize(_tasks, _board, _taskTemplates, _winIcon, _loseIcon, _playerTask);
            initializeCallback?.Invoke();
            _presenter = new GamePresenter(_model, _game, _taskBoard);

            _stopper.Initialize();
            _screen.Initialize(_stopper);
            _slider.Initialize();
            _compass.Initialize(_markers);

            IReadOnlyCharacteristics characteristics = _saveService.Load();
            IDamageable damageable = new Reducer(new Damage(), characteristics.Resistance);
            List<IAction> actions = _actionsBars.Cast<IAction>().ToList();

            _health.Initialize(damageable, characteristics.Health);
            _player.Initialize(_saveService.Load(), actions);
            _tank.Initialize(characteristics.Water);

            _game.Initialize(_rewardWin, _rewardLose, _menuButtons, _pause, _release, _screen, _prepareDelay);

            _presenter.Enable();

            _game.Launch();
        }

        private void OnDestroy()
        {
            _presenter.Disable();
        }
    }
}