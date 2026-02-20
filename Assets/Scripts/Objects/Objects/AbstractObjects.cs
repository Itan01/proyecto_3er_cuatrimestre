using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class AbstractObjects : MonoBehaviour
{
    protected Animator _animator;
    protected Transform _player;
    protected MeshRenderer _mesh;
    protected Collider _collider;
    [SerializeField] protected bool _animated;
    protected ParticlesManager _particlesManager;
    [SerializeField] protected float _distanceToAnimate=10.0f;

    protected void Awake()
    {
        _collider=GetComponent<Collider>();
        _mesh = GetComponentInChildren<MeshRenderer>();
       // GetComponentInParent<RoomManager>().AddToList(this);
    }
    protected virtual void Start()
    {
        _animator = GetComponent<Animator>();
        _particlesManager = GetComponentInChildren<ParticlesManager>();
        _player = GameManager.Instance.PlayerReference.transform;
    }
    protected virtual void Update()
    {
        if ((_player.position - transform.position).magnitude <= _distanceToAnimate)//Show Animation
            _animated = true;
        else
            _animated = false;
        //SetFeedback(_animated);
    }

    protected virtual void SetFeedback(bool State)
    {
    }

    protected virtual void DesactivateObject()
    {
        _collider.enabled = false;
        _mesh.gameObject.SetActive(false);
    }
}
