using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButtonExample : MonoBehaviour, IInteractableObject
{
    private MainDoorManagerExample _doorManager;
    void Start()
    {
        _doorManager = GetComponentInParent<MainDoorManagerExample>();
    }

    public void OnInteract()
    {
        _doorManager.CheckStatus();
    }
}
