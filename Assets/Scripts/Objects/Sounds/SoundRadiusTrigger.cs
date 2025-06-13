using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRadiusTrigger : MonoBehaviour
{
    private float _size=1, _multiplier=5;
    private void Update()
    {
        transform.localScale = new Vector3(_size, _size, _size);
        _size += Time.deltaTime * _multiplier;
    }
    protected void OnTriggerEnter(Collider Entity)
    {
        if (Entity.TryGetComponent<AbstractEnemy>(out AbstractEnemy Script))// sigue el sonido, si el enemigo persigue al jugador, ignora el sonido
        {
            if (Script.GetMode() != 1)
                Script.SetPosition(transform.position);
            if(Script.GetMode()!=2)
                Script.SetModeByIndex(5);
        }
    }

    public void SetMultiplier(float Multiplier)
    {
        if (Multiplier == 0)
            Multiplier = 1;
        _multiplier = Multiplier;
    }
}

