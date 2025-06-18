using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundReferences : MonoBehaviour
{
   [SerializeField] private SoundStruct[] _soundComponents;

    private void Start()
    {
        GameManager.Instance.SoundsReferences = this;
    }
    public SoundStruct GetSoundComponents(int Index)
    {
        return _soundComponents[Index];
    }
}
