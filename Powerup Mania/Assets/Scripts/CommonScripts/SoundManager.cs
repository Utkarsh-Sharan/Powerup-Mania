using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance { get { return _instance; } }

    [SerializeField] private AudioSource _soundEffect;
    [SerializeField] private AudioSource _soundBgMusic;

    [SerializeField] private SoundType[] _sounds;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        PlayMusic(Sounds.BACKGROUND_MUSIC);
    }

    public void PlayMusic(Sounds sound)
    {
        AudioClip clip = getAudioClip(sound);
        if (clip != null)
        {
            _soundBgMusic.clip = clip;
            switch (sound)
            {
                case Sounds.PLAYER_ABRUPT_LEVEL_END:
                    _soundBgMusic.PlayOneShot(_soundBgMusic.clip);
                    break;

                case Sounds.BACKGROUND_MUSIC:
                    _soundBgMusic.Play();
                    break;
            }
        }
        else
        {
            Debug.LogError("Clip not found for: " + sound);
        }
    }

    public void Play(Sounds sound)
    {
        AudioClip clip = getAudioClip(sound);
        if (clip != null)
        {
            _soundEffect.PlayOneShot(clip);
        }
        else
        {
            Debug.LogError("Clip not found for: " + sound);
        }
    }

    private AudioClip getAudioClip(Sounds sound)
    {
        SoundType item = Array.Find(_sounds, soundItem => soundItem.soundType == sound);

        if (item != null)
        {
            return item.soundClip;
        }
        return null;
    }
}

[Serializable]
public class SoundType
{
    public Sounds soundType;
    public AudioClip soundClip;
}

public enum Sounds
{
    BACKGROUND_MUSIC,
    BUTTON_CLICK,

    EXPLOSION_SFX,

    PLAYER_COLLECTED_POWERUP,
    PLAYER_SHOT_LASER,
    PLAYER_ABRUPT_LEVEL_END
}