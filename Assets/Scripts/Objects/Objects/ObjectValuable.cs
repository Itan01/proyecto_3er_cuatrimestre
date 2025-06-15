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
            Destroy(gameObject);
        }

    }
    protected override void SetFeedback(bool State)
    {
        if (State)
            _particlesManager.StartPlay();
        else
            _particlesManager.StopPlay();
        _animator.SetBool("Shine", _animated);
    }
}
