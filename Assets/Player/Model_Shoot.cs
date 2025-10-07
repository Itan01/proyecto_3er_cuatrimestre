using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Model_Shoot : Abstract_Model
{
    Transform _steer, _start;
    Vector3 _auxDir;
    private float _distance = 500f, _radius = 0.2f;
    private SO_Layers _layer;

    public Model_Shoot()
    {
        _start = Camera.main.transform;
        _steer = Camera.main.transform;
    }
    public void Aim()
    {
        Ray Ray = new Ray(_start.position, _steer.forward);
        if (Physics.SphereCast(Ray, _radius, out RaycastHit Hit, _distance, _layer._obstacles, QueryTriggerInteraction.Ignore))
        {
            Vector3 Dir = (Hit.point - _start.position).normalized;
            _auxDir = Dir;
        }
    }

    public override void Execute()
    {
    }

    public void Shoot()
    {
    }
}
