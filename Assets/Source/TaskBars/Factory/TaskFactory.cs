using System;
using System.Collections.Generic;
using UnityEngine;

namespace TaskBars
{
    public class TaskFactory
    {
        private readonly Dictionary<Type, TaskView> Templates;
        private readonly RectTransform Parent;
        private readonly Setup OnCreate;

        public delegate TaskView Setup(TaskView prefab, RectTransform parent);
        
        public TaskFactory(Dictionary<Type, TaskView> templates, RectTransform parent, Setup onCreate)
        {
            Templates = templates;
            Parent = parent;
            OnCreate = onCreate;
        }
        
        public T Create<T>(Type type) where T: TaskView
        {
            return OnCreate?.Invoke(Templates[type], Parent) as T;
        }
    }
}
