using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class RoombaEnemy : MonoBehaviour
{
    [SerializeField] private float _explodeDistance = 2.0f;
    [SerializeField] private float _timeBeforeExplosion = 2.0f;
    private Animator _animator;
    [SerializeField] private GameObject _explosionParticlesPrefab;
    [SerializeField] private GameObject _soundExplosionPrefab;
    [SerializeField] private int _amountOfSounds = 3;
    [SerializeField] private float _soundForce = 5f;
    [SerializeField] private float _spread = 1.5f;
    private RoomManager _room;
    Action Momevent;
    private NavMeshAgent _agent;

    private void Start()
    {
        Momevent = null;
        _animator = GetComponentInChildren<Animator>();
        _agent=GetComponent<NavMeshAgent>();
        _animator.SetBool("Open_Anim", false);  
        _animator.SetBool("Walk_Anim", false);
        _room=GetComponentInParent<RoomManager>();
        _room.DetPlayer += SetActivation;
        _room.ActRoom += Activation;
        _room.DesActRoom += Desactivation;
        if (!_room.IsRoomActivate()) 
        {
            Desactivation();
        }

    }

    private void Update()
    {
        if (Momevent != null)
            Momevent();
    }
    public void SetActivation()
    {
        GetComponentInParent<RoomManager>().DetPlayer -= SetActivation;
        StartCoroutine(OpenAndThenWalk());
    }

    private IEnumerator OpenAndThenWalk()
    {
        _animator.SetBool("Open_Anim", true);
        yield return new WaitForSeconds(1.5f);
        _animator.SetBool("Open_Anim", false);
        _animator.SetBool("Walk_Anim", true);
        _agent.destination =GameManager.Instance.PlayerReference.transform.position;
        Momevent = Moving;
    }
    private void Moving()
    {
        _agent.destination =GameManager.Instance.PlayerReference.transform.position;
        if (_agent.remainingDistance <= _explodeDistance)
        {
            Momevent = null;
            _agent.isStopped = true;
            _animator.SetBool("Walk_Anim", false);

            StartCoroutine(ExplodeAfterDelay());
        }
    }

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(_timeBeforeExplosion);
        Explode();
    }

    private void Explode()
    {
        GetComponentInParent<RoomManager>().DetPlayer -= SetActivation;
        GetComponentInParent<RoomManager>().ActRoom -= Activation;
        GetComponentInParent<RoomManager>().DesActRoom -= Desactivation;
        AudioStorage.Instance.RoombaExplosion();
        // particulas explosion
        if (_explosionParticlesPrefab)
        {
            Instantiate(_explosionParticlesPrefab, transform.position, Quaternion.identity);
        }

        // tirar sonido 
        for (int i = 0; i < _amountOfSounds; i++)
        {
            if (_soundExplosionPrefab)
            {
                GameObject sound = Instantiate(_soundExplosionPrefab, transform.position, Quaternion.identity);

                // la direccion que tira los sonidos (vertical)
                Vector3 randomDir = Vector3.up + new Vector3(
                   UnityEngine.Random.Range(-_spread, _spread),
                    0f,
                   UnityEngine.Random.Range(-_spread, _spread)
                ).normalized;

                if (sound.TryGetComponent(out Rigidbody rb))
                {
                    rb.AddForce(randomDir * _soundForce, ForceMode.Impulse);
                }
            }
        }
        Destroy(gameObject);
    }

    public void Desactivation() 
    {
        gameObject.SetActive(false);
    }
    public void Activation()
    {
        gameObject.SetActive(true);
    }
}