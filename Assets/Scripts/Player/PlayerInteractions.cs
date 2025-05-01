using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private KeyCode _interactButton;
    [SerializeField] private float _interactRayDistance = 10.0f;
    [SerializeField] private float _intRadius = 0.75f;
    [SerializeField] private LayerMask _interactRayMask;
    [SerializeField] private Ray _interactRay;
    [SerializeField] private RaycastHit _intHit;
    [SerializeField] private Transform _rayOrigin,_rayOrientation;
    [SerializeField] private bool _objectCheck;

    private void Update() 
    {
        if (Input.GetKeyDown(_interactButton))
        {
            Interact();
        }
        
    }

    public void Interact()
    {
        _interactRay = new Ray(_rayOrigin.position,_rayOrientation.position);

        if (Physics.SphereCast(_interactRay, _intRadius, out _intHit, _interactRayDistance, _interactRayMask))
        {
            Debug.Log($"Collided obj : {_intHit.collider.name}.");

            if (_intHit.collider.TryGetComponent(out IInteractableObject interactable))
            {
                interactable.OnInteract();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawLine(_interactRay.origin, _interactRay.direction * _interactRayDistance);


    }
}
