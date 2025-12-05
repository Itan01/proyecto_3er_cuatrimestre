using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Gun_particles_Grabbing : MonoBehaviour,IObserverMegaphone
{
   [SerializeField] private VisualEffect _effect;
   [SerializeField] private Transform _megaphone;
    private bool _isGrabbing=false;
    [SerializeField] private AudioClip _clipGrabbing;
    private Transform _myTransform, _cameraTransform;
    private void Start()
    {
        LVLManager.Instance.Gun.AddObs(this);
        _effect = GetComponent<VisualEffect>();
        _effect.Stop();
        _myTransform = transform;
        _cameraTransform = GameManager.Instance.CameraReference.transform;
    }
    private void Update()
    {
        if (!_isGrabbing) return;
        _myTransform.position= _megaphone.position;
        _myTransform.forward= _cameraTransform.forward;
    }
    public void Grabbing(bool State)
    {
        if(!_isGrabbing && State)
        {
            _isGrabbing=true;
            _effect.SendEvent("OnGrabPlay");
            _effect.Play();
            StartCoroutine(PlayAudioOnLoop());
            
        }
        if (!State == _isGrabbing)
        {
            _effect.SendEvent("OnGrabStop");
            _isGrabbing = false;

        }
    }
    private IEnumerator PlayAudioOnLoop()
    {
        while (_isGrabbing)
        {
            AudioManager.Instance.PlaySFX(_clipGrabbing, 1.0f);
            yield return new WaitForSeconds(3.5f);
        }
    }
    public  void Aiming() { }
}
