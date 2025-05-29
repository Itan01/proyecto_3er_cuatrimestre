using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingEnemy : MonoBehaviour
{
    public GameObject soundLoud;
    public string playerTag = "Player";
    private Rigidbody _rb;

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Dentro");
        if (other.CompareTag(playerTag)) 
        {
            if(soundLoud != null)
            {
                soundLoud.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            if(soundLoud != null)
            {
                soundLoud.SetActive(false);
            }
        }
    }
}
