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

    // Update is called once per frame
    protected override void Update()
    {
        if (_enable)
            CheckIfHasArrive();
    }

    protected override void FixedUpdate()
    {
        if (_enable)
        {
            transform.LookAt(new Vector3(_positionSequence[_sequenceIndex].position.x, transform.position.y, _positionSequence[_sequenceIndex].position.z));
            Move();
        }
    }


    private bool DistanceToNextPosition(Transform NextPosition)
    {
        Vector3 _actualPosition = transform.position;

        _actualPosition = NextPosition.position - _actualPosition;
        if (_actualPosition.magnitude <= 0.4f)
        {
            return true; // AVISA QUE LLEGO A SU POSICION
        }
        else
        {
            _dir = _actualPosition; // AVISA QUE NO LLEGO A SU POSICION, POR ENDE DEVUELVE LA DIRECCION A LA QUE DEBE DE IR;
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
