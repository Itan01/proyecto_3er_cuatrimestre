using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SaveData_Configuration : ISaveData
{
    public float SFXVolume;
    public float MusicVolume;

    //public ELanguage Language;
    public void ApplyData()
    {
        SFXVolume = 0.5f;
        MusicVolume = 0.7f;

       // Language = ELanguage.english;
    }
}
