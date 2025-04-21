using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchTheSound : MonoBehaviour
{
    private AbsStandardSoundMov _scriptSound;

     void Update()
    {
        if(Input.GetMouseButtonUp(1))
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider sound)
    {
        if (sound.gameObject.CompareTag("Sound"))
        {
            _scriptSound = sound.GetComponent<AbsStandardSoundMov>();
            _scriptSound.SetTarget(transform, 1000.0f);
        }
    }
    void OnTriggerExit(Collider sound)
    {
        if (sound.gameObject.CompareTag("Sound"))
        {
            _scriptSound = sound.GetComponent<AbsStandardSoundMov>();
            _scriptSound.SetTarget(null, 0.0f);
        }
    }
}
