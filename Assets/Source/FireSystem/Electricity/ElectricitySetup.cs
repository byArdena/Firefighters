using ActionBars;
using UnityEngine;

namespace FireSystem.Electricity
{
    public class ElectricitySetup : MonoBehaviour
    {
        [SerializeField] private ElectricitySwitcher _switcher;
        [SerializeField] private ElectricityPoint[] _points;

        [SerializeField] private float _delay;
        [SerializeField] private float _interactTime;
        
        private Electricity _model;
        private ElectricityPresenter _presenter;

        private void OnDestroy()
        {
            _presenter.Disable();
        }

        public void Initialize(ActionSlider slider)
        {
            _model = new Electricity(_points);
            _presenter = new ElectricityPresenter(_model, _switcher);

            foreach (ElectricityPoint point in _points)
                point.Initialize(_delay);
            
            _switcher.Initialize(slider, _interactTime);
            
            _presenter.Enable();
        }
    }
}