using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class AimingController : MonoBehaviour
{
    private void Update()
    {
        transform.forward = Camera.main.transform.forward;
    }
}
