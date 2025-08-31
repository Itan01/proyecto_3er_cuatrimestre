using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectValuable : AbstractObjects, IInteractableObject
{
    [SerializeField] private int _value;
    [SerializeField] private AudioClip _pickupSound; 
    [SerializeField] private float _soundVolume = 0.80f;

    protected override void Start()
    { 
        base.Start();
    }

    protected override void Update()
    {
        base.Update();  
    }
    public void OnInteract()
    {
        UIManager.Instance.AddScore(_value);

        if (_pickupSound != null)
        {
            //AudioManager.Instance.PlaySFX(_pickupSound, _soundVolume);
            AudioStorage.Instance.SoundsGameObject(EnumAudios.GrabObject);
        }

        DesactivateObject();

    }
    protected override void SetFeedback(bool State)
    {
        if (State)
            _particlesManager.StartLoop();
        else
            _particlesManager.StopLoop();
        _animator.SetBool("Shine", _animated);
    }
    protected override void DesactivateObject()
    {
        _collider.enabled = false;
        gameObject.SetActive(false);
    }
}
