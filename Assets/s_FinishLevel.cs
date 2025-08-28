using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_FinishLevel : MonoBehaviour, IInteractableObject
{
    public void OnInteract()
    {
        UIManager.Instance.FinishLevel();
    }

}
