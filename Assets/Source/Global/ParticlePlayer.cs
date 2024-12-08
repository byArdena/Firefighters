using System;
using System.Collections;
using UnityEngine;

public class ParticlePlayer : MonoBehaviour, IPlayable
{
    private ParticleSystem _particle;
    private AudioSource _sound;
    private float _delay;

    private Coroutine _routine;
    private bool _isAllowPlay;
    private bool _canPlay;
    
    public event Action StartPlaying;
    public event Action StopPlaying;
    public event Action Played;

    public bool CanPlay => _canPlay && _isAllowPlay;

    public void Initialize(ParticleSystem particle, AudioSource sound, float delay)
    {
        _particle = particle;
        _sound = sound;
        _delay = delay;
        _canPlay = true;
        _isAllowPlay = true;
    }
    
    public void AllowPlay()
    {
        _isAllowPlay = true;
    }

    public void DisallowPlay()
    {
        _isAllowPlay = false;
        Stop();
    }
    
    public void Play()
    {
        if (_isAllowPlay == false)
            return;

        _canPlay = false;
        _sound.Play();
        _particle.Play();
        StartPlaying?.Invoke();
        
        _routine = StartCoroutine(PlayRoutine());
    }

    public void Stop()
    {
        _canPlay = true;
        _sound.Stop();
        _particle.Stop();
        StopPlaying?.Invoke();
            
        if (_routine != null)
            StopCoroutine(_routine);
    }
    
    private IEnumerator PlayRoutine()
    {
        bool isWorking = true;
        var wait = new WaitForSeconds(_delay);
            
        while (isWorking)
        {
            Played?.Invoke();
            yield return wait;
        }
    }
}