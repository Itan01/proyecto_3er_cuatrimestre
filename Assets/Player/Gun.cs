using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Abstract_Weapon, IObservableShoot, IObservableGrabSound
{
    private bool _aiming=false;
    [SerializeField]private Factory<Sound_Crash> _factory;
    private SO_Layers _data;
    private RaycastHit _hit;
    private void Start()
    {
        LVLManager.Instance.Gun = this;
    }

    public void Update()
    {
        if (UseRightClick) // Catch Sound
        {
            PrimaryFire();
        }
        else if (UseLeftClick) // Throw Sound
        {
            SecondaryFire();
        }
        transform.forward = Camera.main.transform.forward;
    }
    private void PrimaryFire()
    {
        if (_aiming) return;
        if (Physics.SphereCast(transform.position, 2.0f, transform.forward, out _hit, 10.0f, _data._sounds))
        {
            if(_hit.collider.gameObject.TryGetComponent<Abstract_Sound>(out Abstract_Sound Sound))
            {
                Cons_Raycast ray = new Cons_Raycast(500.0f, _data._obstacles);
                if (ray.Checker<Abstract_Sound>(transform.position, transform.forward))
                {
                    Sound.CanCatch = true;
                    Sound.Atractted = true;
                    Sound.ForceDirection(transform.position - Sound.transform.position);
                }
            }
        }
    }
    private void SecondaryFire()
    {
        if (!_hasBullet) return;
        _hasBullet =false;
        var x = _factory.Create();
        x.transform.position = transform.position;
        x.ForceDirection(Camera.main.transform.forward);
        x.Speed(10.0f);
        x.Size(1.0f);
    }
    //[SerializeField] private LayerMask _enviromentMask;
    //private float _grabbingTimer = 3f;
    //private float _sinceLastPlayed = 3f;
    //private AudioClip _clip;

    //private void Start()
    //{
    //    _clip = AudioStorage.Instance.GunSound(EAudios.GunGrabbing);
    //}
    //private void Update()
    //{
    //    transform.forward = Camera.main.transform.forward;
    //    if (_sinceLastPlayed >= _grabbingTimer)
    //    {
    //        AudioManager.Instance.PlaySFX(_clip, 0.8f);
    //        _sinceLastPlayed = 0f;
    //    }
    //    else
    //    {
    //        _sinceLastPlayed += Time.deltaTime;
    //    }
    //}
    //public void Desactivate()
    //{
    //    if (gameObject.activeSelf)
    //        StartCoroutine(SetActivateFalse());
    //    _sinceLastPlayed = 2.9f;
    //}
    //private IEnumerator SetActivateFalse()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    gameObject.SetActive(false);
    //}

    public Factory<Sound_Crash> Factory
    {
        get { return _factory; }
        set { _factory = value; }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Abstract_Sound>(out Abstract_Sound Sound))
        {
            if(Sound.Atractted && Sound.CanCatch)
            {
                _hasBullet = true;
                Sound.Atractted = false;
                Sound.CanCatch=false;
                Sound.Refresh();
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color= Color.yellow;
        Gizmos.DrawLine(transform.position, _hit.point.magnitude *transform.forward);
        Gizmos.DrawWireSphere(_hit.point,2.0f);
    }
    public void AddObsShoot(IObserverShoot Obj)
    {
        throw new System.NotImplementedException();
    }

    public void RemoveObsShoot(IObserverShoot Obj)
    {
        throw new System.NotImplementedException();
    }

    public void AddObsGrab(IObserverGrabSound Obj)
    {
        throw new System.NotImplementedException();
    }

    public void RemoveObsGrab(IObserverGrabSound Obj)
    {
        throw new System.NotImplementedException();
    }
}
