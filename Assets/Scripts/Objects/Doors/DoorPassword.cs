using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPassword : MonoBehaviour
{
    private void OnCollisionEnter(Collision Door)
    {
        if (Door.gameObject.CompareTag("PasswordDoor"))
        {
            Destroy(Door.gameObject);
            Destroy(gameObject);
        }
    }
}
