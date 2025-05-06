using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyConfused : AbsEnemyVariables
{
    [SerializeField] private float _confuseTime = 6.0f;
    [SerializeField] private GameObject _confusedIcon;

    private float _timer;
    private bool _active;
    private EnemyMovPattern _scriptPattern;
    private EnemyMovFollowTarget _scriptFollow;

    protected override void Start()
    {
        base.Start();
        if (_confusedIcon != null) _confusedIcon.SetActive(false);
        _scriptPattern = GetComponent<EnemyMovPattern>();
        _scriptFollow = GetComponent<EnemyMovFollowTarget>();
    }

    protected override void Update()
    {
        if (!_active) return;

        _timer -= Time.deltaTime;

        // Detener movimiento
        if (_rb != null)
        {
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }

        // Rotar visualmente
        transform.Rotate(Vector3.up * 360f * Time.deltaTime);

        if (_timer <= 0)
        {
            _active = false;
            if (_confusedIcon != null) _confusedIcon.SetActive(false);
            GetComponent<EnemyController>().SetTypeOfMovement(1); // Vuelve a patrullar
        }
    }

    public override void SetActivate(bool mode)
    {
        base.SetActivate(mode);
        _active = mode;

        if (mode)
        {
            _timer = _confuseTime;
            if (_confusedIcon != null) _confusedIcon.SetActive(true);

            // Detener los otros comportamientos
            if (_scriptPattern != null) _scriptPattern.SetActivate(false);
            if (_scriptFollow != null) _scriptFollow.SetActivate(false);
        }
        else
        {
            if (_confusedIcon != null) _confusedIcon.SetActive(false);
        }
    }
}
