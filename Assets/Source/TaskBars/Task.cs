using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace TaskBars
{
    public abstract class Task : MonoBehaviour, IProgress
    {
        [SerializeField] protected Sprite Sprite;
        [SerializeField] protected string Description;
        
        protected Image Image;
        private bool _isWin;
        private bool _isLose;
        private RectTransform _transform;
        private Vector2 _scale;

        public bool IsWin => _isWin;
        
        public event Action<Task> Winning;
        public event Action<Task> Losing;
        
        public void Report(int currentProgress, int maxProgress)
        {
            if (IsTaskCompletedOrFailed() == true)
            {
                return;
            }
            
            DrawProgress(currentProgress, maxProgress);
        }

        public void Win()
        {
            if (IsTaskCompletedOrFailed() == true)
            {
                return;
            }
            
            _isWin = true;
            Winning?.Invoke(this);
        }

        public void Lose()
        {
            if (IsTaskCompletedOrFailed() == true)
            {
                return;
            }
            
            _isLose = true;
            Losing?.Invoke(this);
        }

        public void SetSprite(Sprite sprite)
        {
            Image.sprite = sprite;
            _transform = Image.rectTransform;
            _scale = _transform.sizeDelta;
            
            _transform.DOSizeDelta(_scale * (float)ValueConstants.Two, 
                    (float)ValueConstants.One / (float)ValueConstants.Two)
                .OnComplete(() => _transform.DOSizeDelta(_scale, (float)ValueConstants.One / (float)ValueConstants.Two));
        }

        public abstract void Initialize(TaskFactory factory);
        
        protected abstract void DrawProgress(int currentProgress, int maxProgress);
        
        private bool IsTaskCompletedOrFailed()
        {
            if ((_isWin || _isLose) == true)
            {
                return true;
            }

            return false;
        }
    }
}