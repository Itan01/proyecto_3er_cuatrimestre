using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectValuable : AbstractObjects
{
    [SerializeField] private int _value;
    public void OnInteract ()
    {
        GameManager.Instance.SetScore += _value;
        Destroy(gameObject);
    }

    protected override void OnTriggerEnter(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _animator.SetBool("Shine", true);
        }
    }
    protected override void OnTriggerExit(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _animator.SetBool("Shine", false);
        }
    }
}
