using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Abstract_Weapon
{
    [SerializeField] private bool _aiming=false;
    [SerializeField] private Factory<Sound_Crash> _factory;
    [SerializeField] private SO_Layers _data;

    private RaycastHit _hit;
    private void Awake()
    {
        LVLManager.Instance.Gun = this;
    }

    public void Update()
    {
        transform.forward = Camera.main.transform.forward;
        if (_aiming)
        {
            if (!UseLeftClick) // Throw
            {
                Shoot();
            }
            if (UseRightClick) // Cancel
            {
                _aiming=false;
            }
            return;
        }
        else
        {
            if (UseRightClick) // Grabbing Sound
            {
                PrimaryFire();
                return;
            }
            if (UseLeftClick) // Aim
            {
                SecondaryFire();
            }
            if (_obs == null) return;
            foreach (var Obs in _obs)
            {
                Obs.Grabbing(UseRightClick);
            }
        }

    }
    private void PrimaryFire()
    {
        if (_aiming) return;
        if (Physics.SphereCast(transform.position, 2.0f, transform.forward, out _hit, 10.0f, _data._sounds))
        {
            if(_hit.collider.gameObject.TryGetComponent<Abstract_Sound>(out Abstract_Sound Sound))
            {
                Cons_Raycast ray = new Cons_Raycast(500.0f, _data._everything);
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
        _aiming = true;
        Aiming();
        if (_obs == null) return;
            foreach (var Obs in _obs)
        {
            Obs.Grabbing(false);
        }

    }
    private void Shoot()
    {
        _hasBullet = false;
        _aiming = false;
        var x = _factory.Create();
        x.transform.position = transform.position;
        x.ForceDirection(Camera.main.transform.forward);
        x.Speed(10.0f);
        x.Size(1.0f);
        foreach (var Obs in _obs)
        {
            Obs.SetSound(ESounds.none);
        }
    }
    private void Aiming()
    {

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
                foreach (var Obs in _obs)
                {
                    Obs.SetSound(Sound.IndexRef);
                }
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
}
