using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public struct AudioValues
    {
        public string name;
        public float volume;
    }

    private AudioValues _master;
    private AudioValues _effects;
    private AudioValues _music;
    private AudioValues _userInterface;
    [Header("Audio")]
    [SerializeField] private AudioMixer _mixer;
    private AudioSource _source, _musicSource;

    public float MasterVolume { get { return _master.volume; } }

    public float MusicVolume { get { return _music.volume; } }

    public float SFXVolume { get { return _effects.volume; } }

    public float UIVolumen { get { return _userInterface.volume; } }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        _source = GetComponent<AudioSource>();
        _musicSource = GetComponent<AudioSource>();
        GenerateVolumes();
        SetMasterVolume(MasterVolume);
        SetMusicVolume(MusicVolume);
        SetSFXVolume(SFXVolume);
        SetUIVolume(UIVolumen);
    }
    private void GenerateVolumes()
    {
        _master.name = "MasterVolume";
        _master.volume = 0.75f;
        _music.name = "MusicVolume";
        _music.volume = 0.75f;
        _effects.name = "SFXVolume";
        _effects.volume = 0.75f;
        _userInterface.name = "UIVolume";
        _userInterface.volume = 0.75f;
    }
    public void SetMasterVolume(float Value)
    {
        Value = Mathf.Clamp(Value, 0.0001f, 1.0f);
        _mixer.SetFloat(_master.name, Mathf.Log10(Value) * 20.0f);
    }
    public void SetMusicVolume(float Value)
    {
        Value = Mathf.Clamp(Value, 0.0001f, 1.0f);
        _mixer.SetFloat(_music.name, Mathf.Log10(Value) * 20.0f);
    }
    public void SetSFXVolume(float Value)
    {
        Value = Mathf.Clamp(Value, 0.0001f, 1.0f);
        _mixer.SetFloat(_effects.name, Mathf.Log10(Value) * 20.0f);
    }
    public void SetUIVolume(float Value)
    {
        Value = Mathf.Clamp(Value, 0.0001f, 1.0f);
        _mixer.SetFloat(_userInterface.name, Mathf.Log10(Value) * 20.0f);
    }

    public void PlayMusic(AudioClip Clip)
    {
        if (_musicSource.isPlaying) _musicSource.Stop();
        _musicSource.clip=Clip;
        _musicSource.Play();
    }

    public void PlaySFX(AudioClip clip, float volume)
    {
        if (clip != null)
        {
            _source.PlayOneShot(clip, volume);
        }
    }

}