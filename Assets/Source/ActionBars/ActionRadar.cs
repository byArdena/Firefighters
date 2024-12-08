using System.Collections.Generic;
using UnityEngine;

namespace ActionBars
{
    public class ActionRadar : MonoBehaviour
    {
        private List<IAction> _actions;

        private Transform _player;
        private ActionButton _button;
        private IAction _current;
        private float _interactionDistance;
        private bool _canInteract;

        private void Update() => ChangeCurrent();

        public void Initialize(List<IAction> actions, Transform player, ActionButton button, float interactionDistance)
        {
            _actions = actions;
            _player = player;
            _button = button;
            _interactionDistance = interactionDistance;
            _button.Initialize();

            foreach (IAction action in _actions)
                action.Prepare(StopInteraction);
            
            _canInteract = true;
        }

        public void Interact()
        {
            if (_canInteract == false || _current == null)
                return;

            _canInteract = false;
            _current.Play();
        }

        public void StopInteraction()
        {
            if (_canInteract == true || _current == null)
                return;
            
            _current.Cancel();

            if (_current.DidComplete == true)
            {
                _button.Hide();
                _current.Deselect();
                _current = null;
            }
            
            _canInteract = true;
        }

        private void ChangeCurrent()
        {
            if (_canInteract == false)
                return;

            Vector3 playerPosition = _player.position;
            float distance = float.MaxValue;

            foreach (IAction action in _actions)
            {
                if (action.DidComplete == true)
                    continue;
                
                float newDistance = Vector3.Distance(action.GetPosition(), playerPosition);

                if (newDistance > _interactionDistance)
                {
                    if (action == _current)
                    {
                        _current?.Deselect();
                        _button.Hide();
                        _current = null;
                    }
                    
                    continue;
                }
                
                if (distance <= newDistance)
                    continue;
                
                if (_current != action)
                {
                    _current?.Deselect();
                    _current = action;
                    _current.Select();

                    if (_button.Interactable == false)
                        _button.Show();
                }
                
                distance = newDistance;
            }
        }
    }
}