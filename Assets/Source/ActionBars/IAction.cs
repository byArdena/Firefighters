using System;
using UnityEngine;

namespace ActionBars
{
    public interface IAction
    {
        public void Prepare(Action onInteractionComplete);
        public void Play();
        public void Cancel();
        public void Select();
        public void Deselect();
        public Vector3 GetPosition();
        
        public bool DidComplete { get; }
    }
}