using System;
using System.Collections.Generic;
using ActionBars;
using CompassBars;
using FireSystem.Electricity;
using HealthSystem;
using Survivors;
using TaskBars;
using UnityEngine;
using UnityEngine.Serialization;

namespace FireSystem
{
    public class HouseSetup : MonoBehaviour
    {
        [Space, Header("Main")]
        [SerializeField] private FireSpawner _spawner;
        [SerializeField] private Task _taskFireSpawner;
        
        [Space, Header("Additional")]
        [SerializeField] private ElectricitySetup _electricity;
        [SerializeField] private SurvivorSetup _survivor;

        [Space, Header("Out Took")]
        [SerializeField] private List<ActionBar> _interactable;
        [SerializeField] private List<Marker> _markers;
        [SerializeField] private List<Task> _tasks;

        private ActionSlider _slider;
        private Transform _player;
        private Action _call;
        
        public List<ActionBar> Initialize(ActionSlider slider, Transform player, ExitArea exitArea,
            out List<Marker> markers, out List<Task> tasks, out Action initializeCallback)
        {
            _slider = slider;
            _player = player;
            _call = () => _spawner.Initialize(_taskFireSpawner);
                
            if (_electricity != null)
                _electricity.Initialize(_slider);

            if (_survivor != null)
            {
                exitArea.Initialize(out Marker marker);
                _markers.Add(marker);
                
                initializeCallback = () =>
                {
                    _call?.Invoke();
                    _survivor.Initialize(_slider, _player);
                };
            }
            else
            {
                initializeCallback = _call;
            }

            markers = _markers;
            tasks = _tasks;
            return _interactable;
        }
    }
}