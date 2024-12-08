using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ActionBars
{
    public class ActionSlider : GroupSwitcher
    {
        private const string Second = "сек";
        
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Slider _slider;

        public void ChangeValue(float current, float max)
        {
            _slider.value = current / max;
            float left = max - current;
            _text.SetText($"{left:F1} {Second}");
        }
    }
}