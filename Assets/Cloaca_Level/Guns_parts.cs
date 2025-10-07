using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guns_parts :MonoBehaviour, IInteractableObject
{
   [SerializeField] private CounterGetGun _script;
    public void OnInteract()
    {
        _script.Add();
        gameObject.SetActive(false);
    }
}
