using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Sounds
{
    public class SoundChanger : MonoBehaviour
    {
        private const string Master = nameof(Master);
        private const string Music = nameof(Music);
        
        private AudioMixer _mixer;
        private Slider _master;
        private Slider _music;
        private AudioSource _sound;

        public event Action<float> MasterChanged;
        public event Action<float> MusicChanged;

        private void OnDisable()
        {
            _master.onValueChanged.RemoveListener(ChangeMaster);
            _music.onValueChanged.RemoveListener(ChangeMusic);
        }

        public void Initialize(AudioMixer mixer, Slider master, Slider music, AudioSource sound)
        {
            _mixer = mixer;
            _master = master;
            _music = music;
            _sound = sound;
            
            _master.onValueChanged.AddListener(ChangeMaster);
            _music.onValueChanged.AddListener(ChangeMusic);
        }

        public void Load(float masterVolume, float musicVolume)
        {
            _master.value = masterVolume;
            _music.value = musicVolume;
            _sound.Play();
        }

        private void ChangeMaster(float value)
        {
            _mixer.SetFloat(Master, value);
            MasterChanged?.Invoke(value);
        }
        
        private void ChangeMusic(float value)
        {
            _mixer.SetFloat(Music, value);
            MusicChanged?.Invoke(value);
        }
    }
}