using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyMovementTypeOne : MonoBehaviour
{
    [SerializeField] private float _movSpeed=3.5f;
    private Rigidbody _rb;
    private Vector3 _dir;
    [SerializeField] private Transform[] _positionSequence;
    [SerializeField] private int _sequenceIndex = 0;
    [SerializeField] private bool _hasArrive = false;
    [SerializeField] private bool _enable;
    void Start()
    {
        //Si el enemigo no tiene un camino para hacer, se autodestruye 
        if (_positionSequence.Length == 0)
        {
            Destroy(gameObject);
        }
        _rb = GetComponent<Rigidbody>();
        transform.position=_positionSequence[0].position;
        _enable = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_enable)
        {
            CheckIfHasArrive();
        }
    }
    void FixedUpdate()
    {
        if (_enable)
        {
            transform.LookAt(_positionSequence[_sequenceIndex].position);
            _rb.MovePosition(transform.position + _dir.normalized * _movSpeed * Time.fixedDeltaTime);
        }
    }

    private bool DistanceToNextPosition(Transform NextPosition)
    {
        Vector3 _actualPosition = transform.position;

        _actualPosition = NextPosition.position - _actualPosition;
        Debug.Log(_actualPosition.magnitude);
        if (_actualPosition.magnitude <= 0.4f)
        {
            Debug.Log(_actualPosition.magnitude);
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
            transform.LookAt(_positionSequence[_sequenceIndex].position);

        }
    }
    public void SetActivate(bool setting)
    {
        _enable = setting;
    }

}
