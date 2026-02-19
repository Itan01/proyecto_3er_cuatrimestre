using System;
using UnityEngine;
using System.Collections.Generic;

[Serializable]
public class SaveData_Progress : ISaveData
{
    public int Money;
    public float MovementMultiplier;
    public float GunPowerMultiplier;
    public int Ammo;

    public void ApplyData()
    {
        Money = 0;
        MovementMultiplier = 1;
        GunPowerMultiplier = 1;
        Ammo = 1;
    }
}

