using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class probando : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Triggered by: " + other.name);
    }
}


