using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeDetector : MonoBehaviour
{
    [SerializeField] private GameObject SoundReference;
    void OnTriggerEnter(Collider Player)
    {
        if (Player.TryGetComponent(out EntityMonobehaviour Script))
        {
            Script.GameObjectSoundInvoker(SoundReference);
            Script.CoughState(true);
            
        }
    }

    void OnTriggerExit(Collider Player)
    {
    if (Player.TryGetComponent(out EntityMonobehaviour Script))
    {
        Script.CoughState(false);

    }
    }
}