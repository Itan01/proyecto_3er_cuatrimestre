using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectValuable : AbstractObjects, IInteractableObject
{
    [SerializeField] private int _value;
    public void OnInteract ()
    {
        Vector3 player = GameManager.Instance.PlayerReference.transform.position;
        if ((transform.position - player).magnitude < 4.0f)
        {
            GameManager.Instance.SetScore = _value;
            Destroy(gameObject);
        }

    }

    protected void OnTriggerEnter(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _animator.SetBool("Shine", true);
        }
    }
    protected void OnTriggerExit(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _animator.SetBool("Shine", false);
        }
    }
}
