using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions
{
    private float _interactRayDistance = 2.0f, _intRadius = 0.75f;
    private RaycastHit _intHit;
    private Transform _transform;
    private Transform _orientation;
    private Vector3 _offSet= new Vector3(0.0f,1.5f,0.0f);
    private PlayerScore _scriptScore;
    private LayerMask _interactMask;
    public PlayerInteractions(PlayerScore ScriptScore, Transform PlayerTransform, Transform CameraTransform, LayerMask InteractMask)
    {
        _scriptScore = ScriptScore;
        _transform= PlayerTransform;
        _orientation = CameraTransform;
        _interactMask = InteractMask;
    }
    public void Interact()
    {
        Ray _interactRay = new Ray(_transform.position + _offSet, _orientation.forward);
        if (Physics.SphereCast(_interactRay, _intRadius, out _intHit, _interactRayDistance, _interactMask))
        {
           //Debug.Log($"Collided obj : {_intHit.collider.name}.");

            if (_intHit.collider.TryGetComponent(out IInteractableObject interactable))
            {
                interactable.OnInteract(_scriptScore);
            }
        }
    }
}
