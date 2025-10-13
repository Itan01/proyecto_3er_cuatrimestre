using UnityEngine;

public class Model_Dash : Abstract_Model
{
    private float _force;
    public Model_Dash()
    {
        _force = 0.0f;
        _rb = null;
        _transform = null;
    }
    public Model_Dash Force(float Force)
    {
        _force = Force;
        return this;
    }
    public override void Execute()
    {
        Vector3 Dir= _modelTransform.forward;
        _rb.useGravity = false;
        _rb.AddForce((Dir * _force) +(_transform.up *0.1f), ForceMode.Impulse);
    }
}
