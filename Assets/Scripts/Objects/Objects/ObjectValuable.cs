using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectValuable : AbstractObjects, IInteractableObject
{
    [SerializeField] private int _value;

    protected override void Start()
    { 
        base.Start();
    }

    protected override void Update()
    {
        base.Update();  
    }
    public void OnInteract ()
    {
        Vector3 player = GameManager.Instance.PlayerReference.transform.position;
        if ((transform.position - player).magnitude < 4.0f)
        {
            GameManager.Instance.SetScore = _value;
            Destroyed();
        }

    }
    protected override void SetFeedback(bool State)
    {
        if (State)
            _particlesManager.StartLoop();
        else
            _particlesManager.StopLoop();
        _animator.SetBool("Shine", _animated);
    }
}
