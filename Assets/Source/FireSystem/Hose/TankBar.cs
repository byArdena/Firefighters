using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace FireSystem
{
    [RequireComponent(typeof(Slider))]
    public class TankBar : MonoBehaviour
    {
        private int _maxCapacity;
        private Slider _slider;
        private Image _image;
        private Color _start;
        private bool _isFlashing;

        public void Initialize(int maxCapacity, Image image)
        {
            _maxCapacity = maxCapacity;
            _image = image;
            _slider = GetComponent<Slider>();
            _slider.maxValue = _maxCapacity;
            _start = _image.color;
            Maximize();
        }
        
        public void Decrease()
        {
            if (_slider.value <= (float)ValueConstants.Zero)
            {
                Flash();
                return;
            }
                
            _slider.value--;
        }

        public void Maximize()
        {
            _slider.value = _maxCapacity;
        }

        public void Flash()
        {
            if (_isFlashing == true)
                return;
            
            _isFlashing = true;
            float time = (float)ValueConstants.One / (float)ValueConstants.Three;
            _image.DOColor(Color.red, time).OnComplete(() => _image.DOColor(_start, time).OnComplete(AllowFlash));
        }

        private void AllowFlash()
        {
            _isFlashing = false;
        }
    }
}