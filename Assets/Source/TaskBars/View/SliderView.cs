using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TaskBars
{
    public class SliderView: TaskView
    {
        [SerializeField] public TextMeshProUGUI Percent;
        [SerializeField] public Slider Slider;
    }
}