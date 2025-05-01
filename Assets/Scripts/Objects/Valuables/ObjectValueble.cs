using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectValueble : MonoBehaviour , IInteractableObject
{
    public void OnInteract ()
    {
        Destroy(gameObject);
        Debug.Log("Objeto destruido");
    }
}
