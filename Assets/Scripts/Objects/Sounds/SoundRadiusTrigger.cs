using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRadiusTrigger : MonoBehaviour
{
    protected void OnTriggerEnter(Collider Entity)
    {
        if (Entity.TryGetComponent<AbstractEnemy>(out AbstractEnemy Script))// sigue el sonido, si el enemigo persigue al jugador, ignora el sonido
        {
            if (Script.GetMode() != 1)
                Script.SetPosition(transform.position);
            Script.SetModeByIndex(2);
        }
    }
}

