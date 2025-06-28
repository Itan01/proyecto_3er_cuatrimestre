using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions
{
    private float _interactRayDistance = 2.25f, _intRadius = 0.5f;
    private RaycastHit _intHit;
    private Transform _transform, _orientation;
    private Vector3 _offSet= new Vector3(0.0f,1.75f,0.0f);
    private LayerMask _interactMask;   
    public PlayerInteractions(Transform PlayerTransform, Transform CameraTransform, LayerMask InteractMask)
    {
        _transform= PlayerTransform;
        _orientation = CameraTransform;
        _interactMask = InteractMask;
    }
    public void Interact()
    {
        if (Physics.SphereCast(_transform.position + _offSet, _intRadius, _orientation.forward * 1.0f,out _intHit, _interactRayDistance, _interactMask))
        {
          // Debug.Log($"Collided obj : {_intHit.collider.name}.");

            if (_intHit.collider.TryGetComponent(out IInteractableObject interactable))
            {
                interactable.OnInteract();
            }
        }
    }
}
