using System.Collections;
using UnityEngine;

namespace HealthSystem
{
    public class Screamer : MonoBehaviour
    {
        private const float MinPitch = 0.9f;
        private const float MaxPitch = 1.1f;
        
        private AudioSource _source;
        private bool _canPlay;

        public void Initialize(AudioSource source)
        {
            _source = source;
            _canPlay = true;
        }

        public void Play()
        {
            if (_canPlay == false)
                return;
            
            _source.Play();
            _source.pitch = Random.Range(MinPitch, MaxPitch);
            StartCoroutine(Delay());
        }

        private IEnumerator Delay()
        {
            _canPlay = false;
            yield return new WaitForSeconds((float)ValueConstants.One);
            _canPlay = true;
        }
    }
}