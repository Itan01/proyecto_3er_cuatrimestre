using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRadiusTrigger : MonoBehaviour
{
    private float _size=1, _multiplier=5;
    private SphereCollider _collider;

    private void Awake()
    {
        _collider=GetComponent<SphereCollider>();
    }
    private void Update()
    {
        transform.localScale = new Vector3(_size, _size, _size);
        _collider.radius = _size * 1.10f;
        _size += Time.deltaTime * _multiplier;
    }
    protected void OnTriggerEnter(Collider Entity)
    {
        if (Entity.TryGetComponent(out AbstractEnemy Script))// sigue el sonido, si el enemigo persigue al jugador, ignora el sonido
        {
            Vector3 aux = transform.position;
            aux.y= Script.transform.position.y;
            aux += (Script.transform.position -aux).normalized *2;
            if (!Script.GetActivate()) return;
            if(Script.GetMode()!= 3  && Script.GetMode() != 1 && Script.GetMode() != 6)
            {
                Script.SetModeByIndex(5);
                Script.SetPosition(transform.position);
            }
        }
    }

    public void SetMultiplier(float Multiplier)
    {
        if (Multiplier == 0)
            Multiplier = 1;
        _multiplier = Multiplier;
    }
}

