using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TaskBars
{
    public class TaskBoard : MonoBehaviour
    {
        private List<Task> _tasks;
        private RectTransform _board;
        private TaskFactory _taskFactory;
        private Dictionary<Type, TaskView> _templates;
        private Sprite _winIcon;
        private Sprite _loseIcon;
        private Task _player;
        
        public event Action Won;
        public event Action Lost;

        public void Initialize(List<Task> tasks, RectTransform board, Dictionary<Type, TaskView> templates,
            Sprite winIcon, Sprite loseIcon, Task player)
        {
            _tasks = tasks;
            _board = board;
            _templates = templates;
            _winIcon = winIcon;
            _loseIcon = loseIcon;
            _player = player;
            
            _taskFactory = new TaskFactory(_templates, _board, Instantiate);
            SpawnTasks();
        }

        private void OnDestroy()
        {
            foreach (var task in _tasks)
            {
                task.Winning -= OnTaskWinning;
                task.Losing -= OnTaskLosing;
            }
        }

        private void SpawnTasks()
        {
            foreach (var task in _tasks)
            {
                task.Initialize(_taskFactory);

                task.Winning += OnTaskWinning;
                task.Losing += OnTaskLosing;
            }
        }

        private void OnTaskWinning(Task task)
        {
            task.SetSprite(_winIcon);
            
            if (_tasks.Count(task => task.IsWin == true) == _tasks.Count - 1)
            {
                _player.Win();
                Won?.Invoke();
            }
        }

        private void OnTaskLosing(Task task)
        {
            task.SetSprite(_loseIcon);
            Lost?.Invoke();
        }

    }
}