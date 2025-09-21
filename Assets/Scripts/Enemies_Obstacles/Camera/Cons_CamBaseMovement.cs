using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cons_CamBaseMovement: ICamMovement
{
    private Transform _camTransform;
    private float _maxRotation, _minRotation, _rotationRef, _speed;
    private bool _rotateToRight;
    private Cons_CamColorLight _CamColorLight;
    public Cons_CamBaseMovement(Transform CamTransform, float MaxRotation, float MinRotation,Light Light, Renderer RenderLight, Color Color,float Speed =1.0f ,float InitRotation=0.0f)
    {
        _camTransform=CamTransform;
        _maxRotation=MaxRotation;
        _minRotation=-MinRotation;
        _rotationRef =InitRotation;
        _speed=Speed;
        _CamColorLight = new Cons_CamColorLight(RenderLight,Light, Color);
    }

    public void Move()
    {
        if (_rotateToRight)
        {
            _rotationRef += _speed * Time.deltaTime;
            if (_rotationRef > _maxRotation)
            {
                _rotateToRight = false;
            }
        }
        else
        {
            _rotationRef -= _speed * Time.deltaTime;
            if (_rotationRef < _minRotation)
            {
                _rotateToRight = true;
            }
        }
        _camTransform.localEulerAngles = new Vector3(0.0f, _rotationRef, 0.0f);
    }
    public void Setter()
    {
        _rotationRef = 0.0f;
        _CamColorLight.SetCameraColor();
    }
}
