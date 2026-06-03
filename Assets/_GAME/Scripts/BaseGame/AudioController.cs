using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading.Tasks;
using UnityEngine;

public class AudioController : Singleton<AudioController>
{

    public AudioConfig config;
    public AudioSource musicSource;
    private Stack<AudioSource> _audioSourcePool = new();

    public void Init()
    {
        _audioSourcePool = new();
    }

    public void PlayUIMusic()
    {
        musicSource.clip = config.uiMusic; // gan file am thanh de chay
        musicSource.loop = true; // cho phep am thanh lap lai
        musicSource.Play(); // phat am thanh
    }
    public void PlayGameplayMusic()
    {

        musicSource.clip = config.uiMusic;
        musicSource.loop = true; 
        musicSource.Play(); 
    }

    public async void PlaySound(AudioId id)
    {
        var audioItem = config.GetAudioItem(id);
        var audioSource = _audioSourcePool.Count > 0
            ? _audioSourcePool.Pop()
            : Instantiate(musicSource, transform);
        audioSource.gameObject.SetActive(true);
        audioSource.clip = audioItem.clip;
        audioSource.loop = false;
        audioSource.Play();

        await Task.Delay((int)(audioItem.clip.length * 1000));

        audioSource.gameObject.SetActive(false);
        _audioSourcePool.Push(audioSource);
    }
    public enum AudioId
    {
        // UI
        ButtonClick,
        // Gameplay

        StartGame,WinGame,LoseGame,
        SpawnItem, PickItem,
        PutItem, Combo1, Combo2, Combo3,

    }
    [Serializable] 
    public class AudioItem
    {
        public AudioId id;
        public AudioClip clip;
    }
    [Serializable]
    public class AudioConfig
    {
        public AudioClip uiMusic, gameplayMusic;
        public List<AudioItem> listAudioItem;

        public Dictionary<AudioId,AudioItem> _dictAudioItem;

        public AudioItem GetAudioItem(AudioId id)
        {
            if (_dictAudioItem == null || _dictAudioItem.Count == 0)
            {
                _dictAudioItem = new Dictionary<AudioId,AudioItem>();
                foreach(var ai in listAudioItem)
                    _dictAudioItem.TryAdd(ai.id, ai);
            }
            return _dictAudioItem.GetValueOrDefault(id);
        }
    }


}
