using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TaskBars
{
    public class SliderTask: Task
    {
        private TextMeshProUGUI _description;
        private TextMeshProUGUI _percent;
        private Slider _slider;

        public override void Initialize(TaskFactory factory)
        {
            var data = factory.Create<SliderView>(typeof(SliderView));
            _description = data.Description;
            _description.SetText(Description);
            _percent = data.Percent;
            _slider = data.Slider;
            Image = data.Image;
            Image.sprite = Sprite;
        }
        
        protected override void DrawProgress(int currentProgress, int maxProgress)
        {
            var progress= (float)currentProgress / maxProgress;
            _slider.value = progress;
            
            _percent.SetText($"{progress * 100:0}%");
        }
    }
}