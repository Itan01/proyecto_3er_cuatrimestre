using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radiusToHearSound : MonoBehaviour
{
   private AbsStandardSoundMov _scriptSound;
   //private EnemyConfused _scriptConfused;

    private void Start()
    {
       // _scriptConfused = GetComponentInParent<EnemyConfused>();
    }

    void OnTriggerEnter(Collider Sound)
    {
        if (Sound.gameObject.CompareTag("Sound"))
        {
            _scriptSound = Sound.GetComponent<AbsStandardSoundMov>();
            _scriptSound.SetTarget(transform, 7.5f);

            //if (_scriptConfused != null)
            //{
            //    _scriptConfused.SetActivate(true);
            //}

            //Destroy(Sound.gameObject); // Opcional: destruir el sonido al oírlo
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
