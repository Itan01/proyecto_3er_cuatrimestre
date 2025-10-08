using UnityEngine;
public class Control_Interact :Abstract_Control
{
    private float _distance = 6.0f, _radius = 4.0f;
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
        Ray Ray = new Ray(_player.position,_steer.position);
        if (Physics.SphereCast(Ray, _radius, out RaycastHit Hit, _distance, _layer._interact, QueryTriggerInteraction.Ignore))
        {
            if (Hit.collider.TryGetComponent(out IInteractableObject interactable))
            {
                interactable.OnInteract();
                _view.Interact();
            }
        }
    }
}
