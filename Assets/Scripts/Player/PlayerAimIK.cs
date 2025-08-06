using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAimIK : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Animator animator;

    [Header("IK Weights")]
    [Range(0f, 1f)]
    [SerializeField] private float lookWeight = 1f;  
    [Range(0f, 1f)]
    [SerializeField] private float bodyWeight = 0.6f; 
    [Range(0f, 1f)]
    [SerializeField] private float headWeight = 1.0f; 
    [Range(0f, 1f)]
    [SerializeField] private float eyesWeight = 1.0f; 
    [Range(0f, 1f)]
    [SerializeField] private float clampWeight = 0.5f; 

    private void OnAnimatorIK(int layerIndex)
    {
        bool shooting = animator.GetBool("StartShooting");
        bool grabbing = animator.GetBool("Grabbing");

        if (shooting || grabbing )
        {
            // Usa la dirección de la cámara como punto de mira
            Vector3 aimPoint = Camera.main.transform.position + Camera.main.transform.forward * 100f;

            animator.SetLookAtWeight(lookWeight, bodyWeight, headWeight, eyesWeight, clampWeight);
            animator.SetLookAtPosition(aimPoint);
        }
        else
        {
            animator.SetLookAtWeight(0f);
        }
    }
}
