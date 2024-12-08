using System;
using ActionBars;
using UnityEngine;

namespace FireSystem.Electricity
{
    public class ElectricitySwitcher : ActionBar
    {
        private Transform _transform;

        public event Action TurningOff;
        
        public void Initialize(ActionSlider slider, float maxValue)
        {
            Initialize(slider, maxValue, false);
            _transform = transform;
        }

        public override void OnComplete()
        {
            TurningOff?.Invoke();
        }
        
        public override Vector3 GetPosition()
        {
            return _transform.position;
        }
    }
}