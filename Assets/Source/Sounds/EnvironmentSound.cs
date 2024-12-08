using UnityEngine;

namespace Sounds
{
    public class EnvironmentSound : MonoBehaviour
    {
        private const float MinPitch = 0.9f;
        private const float MaxPitch = 1.1f;

        private AudioSource[] _walks;
        private AudioSource[] _ambient;

        private AudioSource _walk;
        private AudioSource _currentAmbient;

        private bool _isWalking;

        public void Initialize(AudioSource[] walks, AudioSource[] ambient)
        {
            _walks = walks;
            _ambient = ambient;

            ChangeWalk(WalkNames.Grass);
            ChangeAmbient(AmbientNames.Environment);
        }

        public void ChangeWalk(WalkNames name)
        {
            if (_walk == _walks[(int)name])
                return;

            if (_walk != null && _walk.isPlaying)
            {
                _walk.Stop();
                _walks[(int)name].Play();
            }
            _walk = _walks[(int)name];
        }

        public void ChangeAmbient(AmbientNames ambient)
        {
            if (_currentAmbient == _ambient[(int)ambient])
                return;

            _currentAmbient?.Stop();
            _currentAmbient = _ambient[(int)ambient];
            _currentAmbient.Play();
        }

        public void PlayWalk()
        {
            if (_isWalking == false)
            {
                _isWalking = true;
                _walk.Play();
            }
            
            _walk.pitch = Random.Range(MinPitch, MaxPitch);
        }

        public void StopWalk()
        {
            _isWalking = false;
            _walk.Stop();
        }
    }
}