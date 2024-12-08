using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace Menu
{
    public class MissionSpawner : ObjectPool
    {
        private const int TimeDivider = 60;

        private readonly List<MissionView> _activeMission = new();
        private readonly List<MissionView> _notActiveMission = new();

        [SerializeField] private float _intervalSpawn;
        [SerializeField] private TextMeshProUGUI _timer;
        [SerializeField] private float _intervalTick;
        [SerializeField] private int _maxMission;
        [SerializeField] private RectTransform _parent;
        [SerializeField] private float _lifeTimeMission = 62f;
        [SerializeField] private List<Mission> _rewards;

        private float _timerValue;
        private Coroutine _tickCoroutine;
        private IFactory<MissionView> _factory;

        public void Initialize()
        {
            _factory = new MissionFactory(() => Pull<MissionView>(_parent), Remove, _lifeTimeMission, _rewards);
            _tickCoroutine = StartCoroutine(TickCoroutine());
            AddMission();
            _timerValue = _intervalSpawn;
        }

        private IEnumerator TickCoroutine()
        {
            bool isWorking = true;
            var wait = new WaitForSeconds(_intervalTick);

            while (isWorking)
            {
                UpdateTimer(_intervalTick);

                foreach (var mission in _activeMission.Where(mission => mission.TryToPush(_intervalTick) == true))
                {
                    _notActiveMission.Add(mission);
                }

                foreach (MissionView mission in _notActiveMission)
                {
                    mission.OnRejectClicked();
                }

                _notActiveMission.Clear();
                yield return wait;
            }
        }

        private void AddMission()
        {
            _activeMission.Add(_factory.Create());
        }

        private void Remove(MissionView mission)
        {
            _activeMission.Remove(mission);
        }

        private void UpdateTimer(float deltaTime)
        {
            _timerValue -= deltaTime;

            if (_timerValue <= 0)
            {
                _timerValue = _intervalSpawn;

                if (_activeMission.Count < _maxMission)
                    AddMission();
            }

            int minutes = Mathf.FloorToInt(_timerValue / TimeDivider);
            int seconds = Mathf.FloorToInt(_timerValue % TimeDivider);

            _timer.SetText(string.Format("{0}:{1:00}", minutes, seconds));
        }
    }
}