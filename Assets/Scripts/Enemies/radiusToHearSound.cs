using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radiusToHearSound : MonoBehaviour
{
   private AbsStandardSoundMov _scriptSound;

    void OnTriggerEnter(Collider Sound)
    {
        if (Sound.gameObject.CompareTag("Sound"))
        {
            _scriptSound = Sound.GetComponent<AbsStandardSoundMov>();
            _scriptSound.SetTarget(gameObject.transform, 5.0f);
        }
    }
    void OnTriggerExit(Collider Sound)
    {
        if (Sound.gameObject.CompareTag("Sound"))
        {
            _scriptSound = Sound.GetComponent<AbsStandardSoundMov>();
            _scriptSound.SetTarget(null, 5.0f);
        }
    }
}
