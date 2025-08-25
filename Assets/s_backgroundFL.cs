using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_backgroundFL : MonoBehaviour
{
    private void OnTriggerExit(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            gameObject.SetActive(false);
        }
    }
}
