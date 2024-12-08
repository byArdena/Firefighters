using System;
using System.Collections;
using System.Collections.Generic;
using Saves;
using Sounds;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Menu
{
    [RequireComponent(typeof(SoundChanger))]
    public class ProgressSetup : MonoBehaviour
    {
        [Header("Progress")]
        [SerializeField] private ProgressionBar<float> _speed;
        [SerializeField] private ProgressionBar<float> _health;
        [SerializeField] private ProgressionBar<float> _resist;
        [SerializeField] private ProgressionBar<int> _water;
        [SerializeField] private PlayerCharacteristics _startCharacteristic;
        [SerializeField] private TextMeshProUGUI _money;
        [SerializeField] private TextMeshProUGUI _level;
        [SerializeField] private TextMeshProUGUI _energy;
        
        [Space, Header("Audio")]
        [SerializeField] private AudioMixer _mixer;
        [SerializeField] private Slider _master;
        [SerializeField] private Slider _music;
        [SerializeField] private AudioSource _sound;

        private SoundChanger _soundChanger;
        
        private IProgressionBar[] _bars;
        private Progress _model;
        private ProgressPresenter _presenter;

        private void OnDestroy()
        {
            _presenter.Disable();
        }

        public IEnumerator Initialize()
        {
            _soundChanger = GetComponent<SoundChanger>();

            _bars = new IProgressionBar[Enum.GetValues(typeof(PurchaseNames)).Length];
            _bars[(int)PurchaseNames.Speed] = _speed;
            _bars[(int)PurchaseNames.Health] = _health;
            _bars[(int)PurchaseNames.Resistance] = _resist;
            _bars[(int)PurchaseNames.Water] = _water;
            
            _model = new Progress(_startCharacteristic);
            _presenter = new ProgressPresenter(_model, _bars, _soundChanger, _money, _level, _energy);
            
            _soundChanger.Initialize(_mixer, _master, _music, _sound); 
            
            _presenter.Enable();
            yield return new WaitForSeconds(0.1f);
            _model.Load();
        }
    }
}