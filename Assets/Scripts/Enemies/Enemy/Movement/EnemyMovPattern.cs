using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovPattern : AbsEnemyVariables
{
    [SerializeField] private Transform[] _positionSequence;
    [SerializeField] private int _sequenceIndex = 0;
    [SerializeField] private bool _hasArrive = false;

    protected override void Start()
    {
        base.Start();

        if (_positionSequence.Length == 0)
        {
            Destroy(gameObject);
        }

        transform.position = _positionSequence[0].position;
        _enable = true;
    }

    protected override void Update()
    {
        if (!_enable) return;
        CheckIfHasArrive();
    }

    protected override void FixedUpdate()
    {
        if (!_enable) return;

        transform.LookAt(new Vector3(
            _positionSequence[_sequenceIndex].position.x,
            transform.position.y,
            _positionSequence[_sequenceIndex].position.z));

        Move();
    }

    public override void SetActivate(bool mode)
    {
        base.SetActivate(mode);

        if (!mode)
        {
            if (_rb != null)
            {
                _rb.velocity = Vector3.zero;
                _rb.angularVelocity = Vector3.zero;
            }
        }
    }

    private bool DistanceToNextPosition(Transform NextPosition)
    {
        Vector3 _actualPosition = transform.position;
        _actualPosition = NextPosition.position - _actualPosition;

        if (_actualPosition.magnitude <= 0.4f)
        {
            return true;
        }
        else
        {
            _dir = _actualPosition;
            return false;
        }
    }

    private void CheckIfHasArrive()
    {
        _hasArrive = DistanceToNextPosition(_positionSequence[_sequenceIndex]);

        if (_hasArrive)
        {
            _sequenceIndex++;
            if (_sequenceIndex >= _positionSequence.Length)
            {
                _sequenceIndex = 0;
            }
        }
    }
}
