using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    [SerializeField] private KeyCode _interactButton;
    [SerializeField] private float _interactRayDistance = 5.0f;
    [SerializeField] private float _intRadius = 0.75f;
    [SerializeField] private LayerMask _interactRayMask;
    [SerializeField] private Ray _interactRay;
    [SerializeField] private RaycastHit _intHit;
    [SerializeField] private Transform _rayOrigin;
    [SerializeField] private Vector3 _offSet= new Vector3(0.0f,1.5f,0.0f);
    private PlayerScore _script;

    private void Start()
    {
        _script = GetComponent<PlayerScore>();
    }
    private void Update() 
    {
        _interactRay = new Ray(_rayOrigin.position + _offSet, _rayOrigin.forward);
        if (Input.GetKeyDown(_interactButton))
        {
            Debug.Log($"Verificando");
            Interact();
        }
        
    }

    private void Interact()
    {

        if (Physics.SphereCast(_interactRay, _intRadius, out _intHit, _interactRayDistance, _interactRayMask))
        {
            Debug.Log($"Collided obj : {_intHit.collider.name}.");

            if (_intHit.collider.TryGetComponent(out IInteractableObject interactable))
            {
                interactable.OnInteract(_script);
            }
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;

        Gizmos.DrawRay(_interactRay);
    }
}
