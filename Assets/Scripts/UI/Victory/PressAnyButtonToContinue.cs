using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressAnyButtonToContinue : MonoBehaviour
{
    [SerializeField] private float _pulseSpeed = 2f;
    [SerializeField] private float _pulseMagnitude = 0.05f;
    private Vector3 _originalScale;

    private void Start()
    {
        _originalScale = transform.localScale;
    }

    private void Update()
    {
        float scalerEffect = 1 + Mathf.Sin(Time.time * _pulseSpeed) * _pulseMagnitude;
        transform.localScale = _originalScale * scalerEffect;
    }
}
