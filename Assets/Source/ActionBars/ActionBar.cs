using System;
using System.Collections;
using UnityEngine;

namespace ActionBars
{
    [RequireComponent(typeof(ObjectHighlighter))]
    public abstract class ActionBar : MonoBehaviour, IAction
    {
        private const float Delay = 0.1f;

        private ObjectHighlighter _highlighter;
        private ActionSlider _slider;
        private Coroutine _routine;
        private float _maxValue;
        private float _currentValue;
        private bool _isRepeatable;
        private bool _isPlaying;
        private bool _didComplete;

        private Action OnInteractionComplete;

        public bool DidComplete => _didComplete;
        
        public void Initialize(ActionSlider slider, float maxValue, bool isRepeatable, float startValue = (float)ValueConstants.Zero)
        {
            _highlighter = GetComponent<ObjectHighlighter>();
            _highlighter.Initialize();
            _slider = slider;
            _maxValue = maxValue;
            _currentValue = startValue;
            _isRepeatable = isRepeatable;
            Deselect();
        }

        public void Prepare(Action onInteractionComplete)
        {
            OnInteractionComplete = onInteractionComplete;
        }
        
        public void Play()
        {
            if (_isPlaying == true)
                return;
            
            OnStart();
            _slider.ChangeValue(_currentValue, _maxValue);
            _slider.Show();
            _isPlaying = true;
            _routine = StartCoroutine(ProgressRoutine());
        }

        public void Cancel()
        {
            if (_isPlaying == false)
                return;
            
            _slider.Hide();
            
            if (_routine != null)
                StopCoroutine(_routine);
            
            _isPlaying = false;
            OnCancel();
        }

        public void Select() => _highlighter.Select();

        public void Deselect() => _highlighter.Deselect();

        public abstract Vector3 GetPosition();

        public virtual void OnStart() {}
        public virtual void OnCancel() {}
        public virtual void OnComplete() {}
        
        private IEnumerator ProgressRoutine()
        {
            var wait = new WaitForSeconds(Delay);

            while (_currentValue < _maxValue)
            {
                yield return wait;
                _currentValue += Delay;
                _slider.ChangeValue(_currentValue, _maxValue);
            }
            
            _slider.Hide();

            if (_isRepeatable == false)
                _didComplete = true;

            _currentValue = (float)ValueConstants.Zero;
            OnComplete();
            OnInteractionComplete?.Invoke();
        }
    }
}