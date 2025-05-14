using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImpactExplosion : MonoBehaviour
{
    [SerializeField] GameObject soundRadiusPrefab;
    private bool hasExploded = false;
    private bool hasBeenThrown = false; 

    public void MarkAsThrown()
    {
        hasBeenThrown = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!hasBeenThrown || hasExploded) return; 

        hasExploded = true;

        if (soundRadiusPrefab != null)
        {
            Instantiate(soundRadiusPrefab, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
