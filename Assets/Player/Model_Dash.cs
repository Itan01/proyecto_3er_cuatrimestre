using UnityEngine;
using UnityEngine.UIElements;

public class Model_Dash : Abstract_Model
{
    private float _force;
    public Model_Dash(float Force=10.0f)
    {
    }
    public Abstract_Model Force(float Force)
    {
        _force = Force;
        return this;
    }
    public override void Execute()
    {
        _transform.position += _transform.up * 0.3f;
        Vector3 Dir= _modelTransform.forward;
        _rb.velocity=Dir * _force;
    }
}
