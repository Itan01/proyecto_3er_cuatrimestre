using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectValueble : MonoBehaviour , IInteractableObject
{
    [SerializeField] private int _value;
    public void OnInteract (PlayerScore script)
    {
        script.SetScore(_value);
        Debug.Log("Objeto destruido");
        Destroy(gameObject);
    }
}
