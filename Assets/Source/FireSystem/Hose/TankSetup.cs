using ActionBars;
using UnityEngine;
using UnityEngine.UI;

namespace FireSystem
{
    public class TankSetup : MonoBehaviour
    {
        [Header("Main Options")]
        [SerializeField] private ParticlePlayer _hose;
        [SerializeField] private int _minCapacity;
        [SerializeField] private int _maxCapacity;
        [SerializeField] private GroupSwitcher _button;
        
        [Space, Header(nameof(TankBar))]
        [SerializeField] private TankBar _bar;
        [SerializeField] private Image _image;
        
        [Space, Header("ParticlePlayer")]
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private AudioSource _sound;
        [SerializeField] private float _delay;
        
        [Space, Header(nameof(TankFiller))]
        [SerializeField] private TankFiller _filler;
        [SerializeField] private ActionSlider _slider;
        [SerializeField] private float _fillUpTime;

        private Tank _model;
        private TankPresenter _presenter;
        
        public void Initialize(int capacity)
        {
            _model = new Tank(_maxCapacity, _minCapacity);
            _presenter = new TankPresenter(_model, _hose, _filler, _bar, _button);
            
            _button.Initialize();
            _bar.Initialize(_maxCapacity, _image);
            _hose.Initialize(_particle, _sound, _delay);
            _filler.Initialize(_slider, _fillUpTime);
            
            _presenter.Enable();
            _model.FillUp(LiquidType.Water);
        }

        private void OnDestroy()
        {
            _presenter.Disable();
        }
    }
}