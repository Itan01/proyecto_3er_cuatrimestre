using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundRadiusTrigger : MonoBehaviour { }
//{
//    [SerializeField] private float tiempoDeVida = 3f; 

//    private void Start()
//    {
//        Destroy(gameObject, tiempoDeVida); 
//    }

//    private void OnTriggerEnter(Collider other)
//    {
//        Debug.Log("Algo entró al radio: " + other.name); 

//        if (other.CompareTag("Enemy"))
//        {
//            Debug.Log("¡Es un enemigo!");

//            // Buscar el EnemyController en el objeto o en sus padres
//            EnemyController controller = other.GetComponent<EnemyController>();
//            if (controller == null)
//                controller = other.GetComponentInParent<EnemyController>();

//            if (controller != null)
//            {
//                Debug.Log("Activando modo confundido desde el controlador...");
//                controller.SetTypeOfMovement(3);
//            }
//            else
//            {
//                Debug.LogWarning("No se encontró el EnemyController en el objeto ni en sus padres.");
//            }
//        }
//        else
//        {
//            Debug.Log("No es un enemigo, tag: " + other.tag);
//        }
//    }
// }
