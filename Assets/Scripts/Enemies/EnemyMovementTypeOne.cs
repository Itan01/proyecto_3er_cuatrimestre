using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyMovementTypeOne : MonoBehaviour
{
    [SerializeField] private float _movSpeed = 3.5f;
    private Rigidbody _rb;
    private Vector3 _dir;
    [SerializeField] private Transform[] _positionSequence;
    [SerializeField] private int _sequenceIndex = 0;
    [SerializeField] private bool _hasArrive = false;
    void Start()
    {
        //Si el enemigo no tiene un camino para hacer, se autodestruye 
        if (_positionSequence.Length == 0)
        {
            Destroy(gameObject);
        }
        _rb = GetComponent<Rigidbody>();    
        _rb.MovePosition(_positionSequence[0].position);
    }

    // Update is called once per frame
    void Update()
    {
        CheckIfHasArrive();
    }
    void FixedUpdate()
    {
        if (!_hasArrive)
        {
            _rb.MovePosition(transform.position + _dir.normalized * _movSpeed * Time.fixedDeltaTime);
        }
    }

    private bool DistanceToNextPosition(Transform NextPosition)
    {
        Vector3 _actualPosition = transform.position;

        _actualPosition = NextPosition.position - _actualPosition;
        if (_actualPosition.magnitude <= 0.3)
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
        if (_hasArrive = DistanceToNextPosition(_positionSequence[_sequenceIndex]))
        {
            _sequenceIndex++;
            if (_sequenceIndex >= _positionSequence.Length)
            {
                _sequenceIndex = 0;
            }
            transform.LookAt(_positionSequence[_sequenceIndex]);
        }
    }

}
