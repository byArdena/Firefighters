using System;
using ActionBars;
using UnityEngine;

namespace FireSystem
{
    public class TankFiller : ActionBar
    {
        private Transform _transform;
        
        public event Action GoingFill;

        public void Initialize(ActionSlider slider, float maxValue)
        {
            Initialize(slider, maxValue, true);
            _transform = transform;
        }
        
        public override void OnComplete()
        {
            GoingFill?.Invoke();
        }

        public override Vector3 GetPosition()
        {
            return _transform.position;
        }
    }
}