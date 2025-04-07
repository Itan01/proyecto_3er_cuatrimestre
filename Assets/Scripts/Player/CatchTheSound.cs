using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatchTheSound : MonoBehaviour
{
    private SoundMov _scriptSound;

     void Update()
    {
        if(Input.GetMouseButtonUp(1))
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter(Collider sound)
    {
        if (sound.gameObject.CompareTag("sound"))
        {
            _scriptSound = sound.GetComponent<SoundMov>();
            _scriptSound.PlayerTarget(gameObject);
        }
    }
    void OnTriggerExit(Collider sound)
    {
        if (sound.gameObject.CompareTag("sound"))
        {
            _scriptSound = sound.GetComponent<SoundMov>();
            _scriptSound.PlayerTarget(null);
        }
    }
}
