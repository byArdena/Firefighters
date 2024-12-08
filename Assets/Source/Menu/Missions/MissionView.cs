using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


namespace Menu
{
    public class MissionView : SpawnableObject
    {
        private const int TimeDivider = 60;
        
        [SerializeField] private TextMeshProUGUI _name;
        [SerializeField] private TextMeshProUGUI _reward;
        [SerializeField] private TextMeshProUGUI _timer;
        [SerializeField] private Image _icon;
        [SerializeField] private Button _acceptButton;
        [SerializeField] private Button _rejectButton;

        private float _lifeTimeValue;
        private Missions _type;
        private int _rewardValue;

        private Action<MissionView> OnPushed;

        private void OnEnable()
        {
            _acceptButton.onClick.AddListener(OnAcceptClicked);
            _rejectButton.onClick.AddListener(OnRejectClicked);
        }

        private void OnDisable()
        {
            _acceptButton.onClick.RemoveListener(OnAcceptClicked);
            _rejectButton.onClick.RemoveListener(OnRejectClicked);
        }


        public MissionView Initialize(Mission mission, float lifeTimeValue, Action<MissionView> onPushed)
        {
            _type = mission.Type;
            _rewardValue = mission.Reward;
            _reward.SetText(_rewardValue.ToString());
            _name.SetText(mission.Name);
            _icon.sprite = mission.Sprite;
            _lifeTimeValue = lifeTimeValue;
            OnPushed = onPushed;
            return this;
        }

        public bool TryToPush(float deltaTime)
        {
            _lifeTimeValue -= deltaTime;

            if (_lifeTimeValue <= 0)
                return true;
            
            UpdateTimer();
            return false;
        }
        
        public void OnRejectClicked()
        {
            Push();
            OnPushed?.Invoke(this);
        }

        private void UpdateTimer()
        {
            int minutes = Mathf.FloorToInt(_lifeTimeValue / TimeDivider);
            int seconds = Mathf.FloorToInt(_lifeTimeValue % TimeDivider);

            _timer.SetText(string.Format("{0}:{1:00}", minutes, seconds));
        }
        
        private void OnAcceptClicked()
        {
            PlayerPrefs.SetInt(nameof(Constants.Reward), _rewardValue);
            PlayerPrefs.SetInt(nameof(Missions), (int)_type);
            PlayerPrefs.Save();
            Debug.Log(_type);
            SceneManager.LoadScene((int)SceneNames.Load);
        }
    }
}