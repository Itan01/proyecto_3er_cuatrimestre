using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    [SerializeField] private SoundGrabbed _sound;
    private void Awake()
    {
        _sound = new SoundGrabbed();    
    }
}
