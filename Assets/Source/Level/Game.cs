using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Screen = Menu.Screen;

namespace Level
{
    public class Game : MonoBehaviour
    {
        private const float RewardScale = 0.1f;
        
        private TextMeshProUGUI _rewardWin;
        private TextMeshProUGUI _rewardLose;
        private Button[] _menuButtons;
        private Button _pause;
        private Button _release;
        private Screen _screen;
        private float _delay;

        public event Action<int> GettingReward;

        private void OnDestroy()
        {
            _pause.onClick.RemoveListener(Pause);
            _release.onClick.RemoveListener(Release);
            
            foreach (Button button in _menuButtons)
                button.onClick.RemoveListener(Load);
        }

        public void Initialize(TextMeshProUGUI rewardWin, TextMeshProUGUI rewardLose, Button[] menuButtons,
            Button pause, Button release, Screen screen, float delay)
        {
            _rewardWin = rewardWin;
            _rewardLose = rewardLose;
            _menuButtons = menuButtons;
            _pause = pause;
            _release = release;
            _screen = screen;
            _delay = delay;

            _pause.onClick.AddListener(Pause);
            _release.onClick.AddListener(Release);
            
            foreach (Button button in _menuButtons)
                button.onClick.AddListener(Load);
        }

        public void Launch()
        {
            _screen.SetWindow((int)Windows.Play);
        }

        public void Win()
        {
            _rewardWin.SetText(PlayerPrefs.GetInt(nameof(Constants.Reward)).ToString());
            StartCoroutine(Prepare(PlayerPrefs.GetInt(nameof(Constants.Reward)), () => _screen.SetWindow((int)Windows.Win)));
        }

        public void Lose()
        {
            int rewardCount = PlayerPrefs.GetInt(nameof(Constants.Reward));
            rewardCount = Mathf.CeilToInt(rewardCount * RewardScale);
            
            _rewardLose.SetText(rewardCount.ToString());
            StartCoroutine(Prepare(rewardCount, () => _screen.SetWindow((int)Windows.Lose)));
        }

        private void Pause()
        {
            _screen.SetWindow((int)Windows.Pause);
        }

        private void Release()
        {
            _screen.SetWindow((int)Windows.Play);
        }

        private void Load()
        {
            SceneManager.LoadScene((int)SceneNames.Menu);
        }

        private IEnumerator Prepare(int rewardCount, Action onReady)
        {
            GettingReward?.Invoke(rewardCount);
            PlayerPrefs.SetInt(nameof(Constants.Reward), (int)ValueConstants.Zero);
            yield return new WaitForSeconds(_delay);
            onReady?.Invoke();
        }
    }
}