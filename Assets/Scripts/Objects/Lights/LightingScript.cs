using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingScript : MonoBehaviour
{
    private void Awake()
    {
        GetComponentInParent<RoomManager>().AddToList(GetComponent<Light>());
        gameObject.SetActive(false);
    }
}
