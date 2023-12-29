using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using UnityEngine;

public class AudioManager : Singleton<AudioManager>
{
    private Dictionary<string, SoundSO> _sounds = new Dictionary<string, SoundSO>();
    private Dictionary<string, Coroutine> _fadeoutRoutines = new Dictionary<string, Coroutine>();

    protected AudioManager() {}

    private void Awake()
    {
        foreach (SoundSO s in Resources.LoadAll<SoundSO>("Sounds"))
        {
            _sounds[s.soundName] = s;
            _fadeoutRoutines[s.soundName] = null;
            s.AddSource(gameObject.AddComponent<AudioSource>());
        }
    }
    public void StopAll()
    {
        foreach (SoundSO sound in _sounds.Values)
        {
            sound.Stop();
        }
    }
    
    public void PauseAll()
    {
        foreach (SoundSO sound in _sounds.Values.Where(sound => sound.IsPlaying()))
        {
            sound.Pause();
        }
    }
    
    public void UnPauseAll()
    {
        foreach (SoundSO sound in _sounds.Values)
        {
            sound.UnPause();
        }
    }
    
    public void Play(string soundName, Action onMusicFinish = null){
        if (!_sounds.ContainsKey(soundName))
        {
            return;
        }
        if (_sounds[soundName].overlap || !_sounds[soundName].IsPlaying())
        {
            if (_fadeoutRoutines[soundName] != null)
            {
                StopCoroutine(_fadeoutRoutines[soundName]);
            }
            _sounds[soundName].ChangeVolume(_sounds[soundName].volume);
            _sounds[soundName].Play();
        }
        if (onMusicFinish != null)
        {
            StartCoroutine(WaitForSound(soundName, onMusicFinish));
        }
    }

    private IEnumerator WaitForSound(string soundName, Action onMusicFinish)
    {
        yield return new WaitUntil(() => _sounds[soundName].IsPlaying() == false);
        onMusicFinish?.Invoke();
    }
    
    public void Stop(string soundName){
        if (!_sounds.ContainsKey(soundName))
        {
            return;
        }
        if (_sounds[soundName].IsPlaying())
        {
            _sounds[soundName].Stop();
        }
    }

    public void StopFadeOut(string soundName, float fadeOutDuration = 2f) 
    {
        _fadeoutRoutines[soundName] = StartCoroutine(StopFadeOutCoroutine(soundName, fadeOutDuration));
    }

    private IEnumerator StopFadeOutCoroutine(string soundName, float fadeOutDuration = 2f) 
    {
        if (!_sounds.ContainsKey(soundName))
        {
            yield break;
        }
        if (!_sounds[soundName].IsPlaying())
        {
            yield break;
        }
        float startVolume = _sounds[soundName].Volume;
        while (_sounds[soundName].Volume > 0f)
        {
            var newVolume = _sounds[soundName].Volume - (startVolume * Time.deltaTime / fadeOutDuration);
            _sounds[soundName].ChangeVolume(newVolume);
            yield return new WaitForEndOfFrame();
        }
        _sounds[soundName].Stop();
        _sounds[soundName].ChangeVolume(startVolume);
    }
}