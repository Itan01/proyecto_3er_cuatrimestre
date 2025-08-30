using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions
{
    private float _interactRayDistance = 4.0f, _intRadius = 5.0f;
    private RaycastHit _intHit;
    private Transform _orientation;
    private LayerMask _interactMask;
    private Animator _animator;
    public PlayerInteractions(Animator animator)
    {
        _orientation = Camera.main.transform;
        _interactMask = LayerManager.Instance.GetLayerMask(EnumLayers.InteractMask);
        _animator= animator;
    }
    public void Interact()
    {
        if (Physics.SphereCast(_orientation.position, _intRadius, _orientation.forward,out _intHit, _interactRayDistance, _interactMask, QueryTriggerInteraction.Ignore))
        {
            if (_intHit.collider.TryGetComponent(out IInteractableObject interactable))
            {
                _animator.SetTrigger("Steal");
                interactable.OnInteract();
            }
        }
    }
}
