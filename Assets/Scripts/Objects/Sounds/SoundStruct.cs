using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public struct SoundStruct
{
    [SerializeField] private GameObject _sound;
    [SerializeField] private Sprite _spriteUi;
    [SerializeField] private int _index;

    public GameObject Sound
    {
       get { return _sound; }
    }
    public Sprite SpriteUi
    {
        get { return _spriteUi; }
    }
    public int Index
    {
        get { return _index; }
    }
}
