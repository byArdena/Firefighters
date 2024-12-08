using System.Collections;
using UnityEngine;

namespace Menu
{
    [RequireComponent(typeof(Stopper))]
    public class MenuBootstrap : MonoBehaviour
    {
        [SerializeField] private WindowSwitcher _switcher;
        [SerializeField] private MissionSpawner _spawner;
        [SerializeField] private ProgressSetup _progressSetup;

        private Stopper _stopper;
        
        private IEnumerator Start()
        {
            _stopper = GetComponent<Stopper>();
            
            _stopper.Initialize();
            _switcher.Initialize(_stopper);
            _stopper.Release();
            yield return _progressSetup.Initialize();
            _spawner.Initialize();
        }
    }
}