using Unity.Mathematics;
using UnityEngine;
public class Control_Interact :Abstract_Control
{
    private float _distance = 6.0f;
    private Vector3 _boxCastSize = new Vector3(10.0f, 10.0f, 0.1f);
    private SO_Layers _layer;
    private Transform _player, _steer;
    public Control_Interact(PL_Control Controller)
    {
        _player = null;
        _steer = Camera.main.transform;
        Controller.AddAction(Execute);
    }

    public Control_Interact Layer(SO_Layers Data)
    {
        _layer= Data;
        return this;
    }
    public Control_Interact SetEntity(Transform Transform)
    { 
        _player= Transform;
        return this;
    }
    public override void Execute()
    {
        if (Input.GetKeyDown(_key))
        {
            Check();
        }
    }

    public void Check()
    {
        if (Physics.BoxCast(_player.position, _boxCastSize , _steer.forward, out RaycastHit Hit, quaternion.identity, _distance, _layer._interact, QueryTriggerInteraction.Ignore))
        {
            if (Hit.collider.TryGetComponent(out IInteractableObject interactable))
            {
                interactable.OnInteract();
                _view.Interact();
            }
        }
    }
}
