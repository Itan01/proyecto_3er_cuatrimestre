using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Gun : Abstract_Weapon
{
    [SerializeField] private bool _aiming=false;
    [SerializeField] private SO_Layers _data;
    private bool _enableGrabbing=true;
    private float _boxSizeValue = 5.0f;
    private Vector3 _boxSize=Vector3.zero;
    private Vector3 _dirToShoot;
    private RaycastHit _hit;
    private Camera _mainCamera;
    private Transform _myTransform;
    private ISoundAim _aimedObject=null;
    [SerializeField] private MeshRenderer _render;
    [SerializeField] private AudioClip _shootClip;
    [SerializeField] private ScriptableRendererFeature _renderFullScreen;
    [SerializeField] private Material _materialFullScreen;

    private void Awake()
    {
        GetComponentInParent<PlayerManager>().Weapon = this;
        LVLManager.Instance.Gun = this;
        _renderFullScreen.SetActive(false);
        _boxSize = new Vector3(_boxSizeValue, _boxSizeValue, 0.1f);
    }
    private void Start()
    {
        _mainCamera = Camera.main;
        _myTransform = transform;
    }
    protected override void Update()
    {
        base.Update();
        _myTransform.forward = _mainCamera.transform.forward;
        if (_aiming)
        {
            Aiming();
            if (!UseLeftClick) // Throw
            {
                Shoot();
                EventPlayer.Trigger(EPlayer.aim, false);
            }
            if (UseRightClick) // Cancel
            {
                _aiming=false;
                _enableGrabbing = false;
                EventPlayer.Trigger(EPlayer.aim, false);
            }
            return;
        }
        else if(_enableGrabbing == true)
        {
            if (_obs == null) return;
            foreach (var Obs in _obs)
            {
                Obs.Grabbing(UseRightClick);
            }
            if (UseRightClick) // Grabbing Sound
            {
                PrimaryFire();
                return;
            }
            if (UseLeftClick) // Aim
            {
                SecondaryFire();
            }
 
        }
        else
        {
        if (!UseRightClick) // Grabbing Sound
                _enableGrabbing = true;
        }


    }
    private void PrimaryFire()
    {
        if (_aiming) return;
        if (Physics.BoxCast(_myTransform.position, _boxSize, _myTransform.forward, out _hit, Quaternion.LookRotation(transform.forward), 10.0f, _data._sounds))
        {
            if(_hit.collider.gameObject.TryGetComponent<Abstract_Sound>(out Abstract_Sound Sound))
            {
                Cons_Raycast ray = new Cons_Raycast(500.0f, _data._everything);
                if (ray.CheckerComponent<Abstract_Sound>(transform.position, Sound.transform.position - transform.position))
                {
                    Sound.CanCatch = true;
                    Sound.Atractted = true;
                    Sound.ForceDirection(_myTransform.position - Sound.transform.position);
                    Sound.Speed(25.0f);
                }
            }
        }
    }
    private void SecondaryFire()
    {
        if (!_hasBullet) return;
        if (!_aiming)
        {
            
            _aiming = true;
            EventPlayer.Trigger(EPlayer.aim, true);
            if (_obs == null) return;
            foreach (var Obs in _obs)
            {
                Obs.Grabbing(false);
            }
        }
    }
    private void Shoot()
    {
        _hasBullet = false;
        _aiming = false;
        var x = Factory_CrashSound.Instance.Create();
        x.transform.position = transform.position + transform.forward * 2.0f;
        x.ForceDirection(_dirToShoot);
        x.Speed(20.0f);
        x.Size(1.0f);
        x.ShootByPlayer=true;
        AudioManager.Instance.PlaySFX(_shootClip,1.0f);
        StartCoroutine(Explosion());
        if (_render != null)
            _render.material.SetFloat("_HasASound", 0.0f);
        foreach (var Obs in _obs)
        {
            Obs.SetSound(ESounds.none);
        }
    }
    private void Aiming()
    {
        float maxDistance=200.0f;
        float DistanceBetweenCamAndPlayer = (_myTransform.position - _mainCamera.transform.position).magnitude;
        Vector3 StartPosition = _mainCamera.transform.position + _mainCamera.transform.forward * DistanceBetweenCamAndPlayer;
        if (Physics.Raycast(StartPosition, _mainCamera.transform.forward, out RaycastHit Hits, maxDistance, _data._obstacles, QueryTriggerInteraction.Ignore))
        {
            _dirToShoot = (Hits.point - _myTransform.position).normalized;
            if (Hits.collider.TryGetComponent<ISoundAim>(out var Script))
            {
                Debug.Log($"Got {Script.ToString()}");
                if (_aimedObject != null)
                    _aimedObject.Activate();
                if (_aimedObject == Script) return;
                Debug.Log($"ChangeObject{Script.ToString()}");
                if (_aimedObject != null)
                _aimedObject.Deactivate();
                Script.Activate();
                _aimedObject= Script;
            }
            else
            {
                if (_aimedObject != null)
                    _aimedObject.Deactivate();
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<Abstract_Sound>(out Abstract_Sound Sound))
        {
            if(Sound.Atractted && Sound.CanCatch)
            {
                if (_render != null)
                    _render.material.SetFloat("_HasASound",1.0f);
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
    private void OnDrawGizmosSelected()
    {
        Gizmos.color= Color.yellow;
        Gizmos.DrawLine(transform.position, _hit.point.magnitude *transform.forward);
        Gizmos.DrawWireCube(_hit.point,_boxSize);
    }
    private IEnumerator Explosion()
    {
        _renderFullScreen.SetActive(true);
        float size = -0.1f;
        while(size < 1.0f)
        {
            size += Time.deltaTime;
            _materialFullScreen.SetFloat("_WaveSize",size);
            yield return null;
        }
        _renderFullScreen.SetActive(false);
        yield return null;

    }
}
