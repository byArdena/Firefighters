using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace TaskBars
{
    public class TextTask : Task
    {
        private TextMeshProUGUI _description;
        private TextMeshProUGUI _progress;

        public override void Initialize(TaskFactory factory)
        {
            var data = factory.Create<TextView>(typeof(TextView));
            _description = data.Description;
            _description.SetText(Description);
            _progress = data.Progress;
            Image = data.Image;
            Image.sprite = Sprite;
        }
        
        protected override void DrawProgress(int currentProgress, int maxProgress)
        {
            _progress.SetText($"{currentProgress}/{maxProgress}");
        }
    }
}