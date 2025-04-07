using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radiusToHearSound : MonoBehaviour
{
    private SoundMov _scriptSound;
    void Start()
    {
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider sound)
    {
        if (sound.gameObject.CompareTag("sound"))
        {
            _scriptSound = sound.GetComponent<SoundMov>();
            _scriptSound.NewEnemyTarget(gameObject);
        }
    }
    void OnTriggerExit(Collider sound)
    {
        if (sound.gameObject.CompareTag("sound"))
        {
            _scriptSound = sound.GetComponent<SoundMov>();
            _scriptSound.NewEnemyTarget(null);
        }
    }
}
