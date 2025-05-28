using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectValuable : MonoBehaviour , IInteractableObject
{
    [SerializeField] private int _value;
    public void OnInteract ()
    {
        GameManager.Instance.SetScore += _value;
        Destroy(gameObject);
    }
}
