using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions
{
    private float _interactRayDistance = 4.0f, _intRadius = 4.0f;
    private RaycastHit _intHit;
    private Transform _orientation;
    private LayerMask _interactMask;   
    public PlayerInteractions( LayerMask InteractMask)
    {
        _orientation = Camera.main.transform;
        _interactMask = InteractMask;
    }
    public void Interact()
    {
        if (Physics.SphereCast(_orientation.position, _intRadius, _orientation.forward,out _intHit, _interactRayDistance, _interactMask, QueryTriggerInteraction.Ignore))
        {
       // Debug.Log($"Collided obj : {_intHit.collider.name}.");

            if (_intHit.collider.TryGetComponent(out IInteractableObject interactable))
            {
                interactable.OnInteract();
            }
        }
    }
}
