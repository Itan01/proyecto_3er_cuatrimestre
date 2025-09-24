using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button_BoxMove : MonoBehaviour, IInteractableObject
{
    [SerializeField]private CloacaWater _water;

    public void OnInteract()
    {
        _water.DryCloaca();
    }
}
