using UnityEngine;
using UnityEngine.UI;

namespace Menu
{
    [RequireComponent(typeof(Screen))]
    public class WindowSwitcher : MonoBehaviour
    {
        [SerializeField] private Button _upgrades;
        [SerializeField] private Button _missions;
        [SerializeField] private Button _volume;
        [SerializeField] private Button[] _closeButtons;

        private Screen _screen;
        
        public void Initialize(Stopper stopper)
        {
            _screen = GetComponent<Screen>();
            _screen.Initialize(stopper);
            ShowMain();
        }

        private void OnEnable()
        {
            _upgrades.onClick.AddListener(ShowUpgrades);
            _missions.onClick.AddListener(ShowMissions);
            _volume.onClick.AddListener(ShowVolume);
            
            foreach (Button button in _closeButtons)
                button.onClick.AddListener(ShowMain);
        }

        private void OnDisable()
        {
            _upgrades.onClick.RemoveListener(ShowUpgrades);
            _missions.onClick.RemoveListener(ShowMissions);
            _volume.onClick.RemoveListener(ShowVolume);

            foreach (Button button in _closeButtons)
                button.onClick.RemoveListener(ShowMain);
        }
        
        public void Hide()
        {
            _screen.Hide();
        }

        private void ShowMain()
        {
            _screen.SetWindow((int)MainMenuWindows.Main);
        }
        
        private void ShowUpgrades()
        {
            _screen.SetWindow((int)MainMenuWindows.Upgrades);
        }

        private void ShowMissions()
        {
            _screen.SetWindow((int)MainMenuWindows.Missions);
        }

        private void ShowVolume()
        {
            _screen.SetWindow((int)MainMenuWindows.Volume);
        }
    }
}