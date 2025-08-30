using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderAction : MonoBehaviour
{
    [SerializeField] private Slider _sliderMaster; 
    [SerializeField] private Slider _sliderMusic;
    [SerializeField] private Slider _sliderSFX;
    [SerializeField] private Slider _sliderUI;

    private void Start()
    {
        _sliderMaster.value = AudioManager.Instance.MasterVolume;
        _sliderMusic.value = AudioManager.Instance.MusicVolume;
        _sliderSFX.value = AudioManager.Instance.SFXVolume;
        _sliderUI.value = AudioManager.Instance.UIVolumen;
    }

    public void SetMasterVolume(float value)
    {
        AudioManager.Instance.SetMasterVolume(value);
    }
    public void SetMusicVolume(float value)
    {
        AudioManager.Instance.SetMusicVolume(value);
    }
    public void SetSFXVolume(float value)
    {
        AudioManager.Instance.SetSFXVolume(value);
    }
    public void SetUIVolume(float value)
    {
        AudioManager.Instance.SetUIVolume(value);
    }
}
