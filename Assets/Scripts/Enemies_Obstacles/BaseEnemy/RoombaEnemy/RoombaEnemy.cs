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
    private bool _isActivate=false;
    [SerializeField] private bool _isRunnning=false;
    Action Momevent;
    private NavMeshAgent _agent;

    private void Start()
    {
        Momevent = null;
        _animator = GetComponentInChildren<Animator>();
        GetComponentInParent<RoomManager>().ActRoom += Activation;
        GetComponentInParent<RoomManager>().DesActRoom += Desactivation;
        _agent =GetComponent<NavMeshAgent>();
        EventManager.Subscribe(EEvents.DetectPlayer, Detect);
        _animator.SetBool("Open_Anim", false);  
        _animator.SetBool("Walk_Anim", false);
        _agent.isStopped = true;
    }

    private void Update()
    {
        if (!_isActivate) return;
        Momevent?.Invoke();
    }
    private void Detect(params object[] Parameters)
    {
        if (!_isRunnning) return;
        EventManager.Unsubscribe(EEvents.DetectPlayer, Detect);
        Transform Pos= (Transform)Parameters[0];
        _agent.destination = Pos.position;

        AudioStorage.Instance.RoombaSound(EAudios.RoombaSpawn);
        StartCoroutine(OpenAndThenWalk());
        _isActivate = true;
    }

    private IEnumerator OpenAndThenWalk()
    {
        _animator.SetBool("Open_Anim", true);
        yield return new WaitForSeconds(3.0f);
        _animator.SetBool("Open_Anim", false);
        _agent.isStopped = false;
        _animator.SetBool("Walk_Anim", true);
        Momevent = Moving;
    }
    private void Moving()
    {   
        if (_agent.remainingDistance <= _explodeDistance)
        {
            Momevent = null;
            _agent.isStopped = true;
            _animator.SetBool("Walk_Anim", false);

            StartCoroutine(ExplodeAfterDelay());
        }
        if (GameManager.Instance.PlayerReference.GetInvisible()) return;
        _agent.destination = GameManager.Instance.PlayerReference.transform.position;
    }

    private IEnumerator ExplodeAfterDelay()
    {
        yield return new WaitForSeconds(_timeBeforeExplosion);
        Explode();
    }

    private void Explode()
    {
        GetComponentInParent<RoomManager>().ActRoom -= Activation;
        GetComponentInParent<RoomManager>().DesActRoom -= Desactivation;
        AudioStorage.Instance.RoombaExplosion();
        // particulas explosion
        if (_explosionParticlesPrefab)
        {
            var x = Factory_Explosion_Crash_Sound.Instance.Create();
            x.transform.position = transform.position;
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
        EventManager.Unsubscribe(EEvents.DetectPlayer, Detect);
        gameObject.SetActive(false);
        _isRunnning = false;
    }
    public void Activation()
    {
        if (_isActivate) return;
        _isRunnning = true;
        EventManager.Subscribe(EEvents.DetectPlayer, Detect);
        gameObject.SetActive(true);
    }
    private void OnDestroy()
    {
        EventManager.Unsubscribe(EEvents.DetectPlayer, Detect);
    }
}