using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConfused : AbsEnemyVariables
{
    [SerializeField] private float _confuseTime = 3.0f;
    private float _timer;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        if (_enable)
        {
            _timer -= Time.deltaTime;

            // Movimiento circular/confuso: podés personalizar
            float angle = Mathf.PingPong(Time.time * _rotSpeed, 360);
            _dir = new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle));

            if (_timer <= 0)
            {
                _enable = false;
                GetComponent<EnemyController>().SetTypeOfMovement(1); // volver al patrullaje
            }
        }
    }

    public override void SetActivate(bool mode)
    {
        base.SetActivate(mode);

        if (mode)
        {
        
            _timer = _confuseTime;
        }
    }

}
