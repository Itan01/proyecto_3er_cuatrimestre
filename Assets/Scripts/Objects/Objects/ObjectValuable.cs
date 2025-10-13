using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectValuable : AbstractObjects, IInteractableObject
{
    [SerializeField] private int _value;
    [SerializeField] private AudioClip _pickUpValueSound;
    [SerializeField] private AudioClip _pickUpFragSound;
    [SerializeField] private float _soundVolume = 0.80f;

    protected override void Start()
    { 
        base.Start();
        _pickUpFragSound = AudioStorage.Instance.SoundsGameObject(EAudios.GrabObject);
        _pickUpValueSound = AudioStorage.Instance.SoundsGameObject(EAudios.GrabObject);
    }

    protected override void Update()
    {
        base.Update();  
    }
    public void OnInteract()
    {
        EventPlayer.Trigger(EPlayer.steal, (float)_value);
        if (_pickUpValueSound != null || _pickUpFragSound != null) 
        {
            //AudioManager.Instance.PlaySFX(_pickupSound, _soundVolume);
            AudioManager.Instance.PlaySFX(_pickUpFragSound, _soundVolume);
            AudioManager.Instance.PlaySFX(_pickUpValueSound, _soundVolume);
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
