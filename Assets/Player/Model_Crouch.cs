using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;
public class Model_Crouch : Abstract_Model
{
    private Model_Move _move;
    private BoxCollider _col;
    private bool _isCrouching=false;
    private float _crouchSpeed;
    private LayerMask _layerMask;   
    public Model_Crouch()
    {
        _crouchSpeed = 0.0f;
        _col = null;

    }
    public Model_Crouch Move(Model_Move Move)
    {
        _move = Move;
        return this;
    }
    public Model_Crouch Speed(float Speed)
    {
        _crouchSpeed = Speed;
        return this;
    }
    public Model_Crouch Collider(BoxCollider Collider)
    {
        _col = Collider;
        return this;
    }
    public Model_Crouch Layer(LayerMask Mask)
    {
        _layerMask = Mask;
        return this;
    }
    public override void Execute()
    {
        if (_isCrouching)
        {
            if (Physics.BoxCast(_transform.position, new Vector3(0.6f, 0.5f, 0.6f), _transform.up, Quaternion.identity, 10.0f, _layerMask) == false)
            {
                _isCrouching = false;
                GameManager.Instance.PlayerReference.ResetCollider();
                _move.ResetSpeed();
            }
        }
        else
        {
            _col.size = new Vector3(0.75f, 1.0f, 0.75f);
            _col.center = new Vector3(0.0f, 0.6f, 0.0f);
            _isCrouching = true;
            _move.Speed(_crouchSpeed);
        }

    }
    public bool GetIsCrouching()
    {
        return _isCrouching;
    }
}
