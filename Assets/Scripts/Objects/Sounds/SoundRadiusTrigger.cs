using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRadiusTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Algo entró al radio: " + other.name); // Log para ver si entra algo

        if (other.CompareTag("Enemy"))
        {
            Debug.Log("¡Es un enemigo!");

            // Busca el script en el objeto o en sus padres
            EnemyConfused confused = other.GetComponent<EnemyConfused>();
            if (confused == null)
                confused = other.GetComponentInParent<EnemyConfused>();

            if (confused != null)
            {
                Debug.Log("Confundiendo al enemigo...");
                confused.SetActivate(true);
            }
            else
            {
                Debug.LogWarning("No se encontró el script EnemyConfused en el objeto ni en sus padres.");
            }
        }
        else
        {
            Debug.Log("No es un enemigo, tag: " + other.tag);
        }
    }
}
