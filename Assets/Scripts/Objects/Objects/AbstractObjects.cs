using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

public class AbstractObjects : MonoBehaviour
{
    protected Animator _animator;
    protected Transform _player;
    [SerializeField] protected bool _animated;
    protected ParticlesManager _particlesManager;
    [SerializeField] protected float _distanceToAnimate=10.0f;

    protected void Awake()
    {
        //GetComponentInParent<RoomManager>().AddToList(this);
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
        SetFeedback(_animated);
    }

    protected virtual void SetFeedback(bool State)
    {
    }

    protected void Destroyed()
    {
        gameObject.SetActive(false);
    }
}
