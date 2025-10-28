using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSummoner : MonoBehaviour
{
    [SerializeField] private float _timer;
    private float _timerRef;
    private int _amount;
    [Range(1, 5)][SerializeField] private int _amountRef;
    private RoomManager _room;
    Action Behaviour;
     private float _distance=1.5f;
    [SerializeField] private Transform _dir;
    private ParticlesManager _particlesManager;
    private void Start()
    {
        _room = GetComponentInParent<RoomManager>();
        _room.ActRoom +=Activate;
        _room.DesActRoom += DesActivate;
        _particlesManager = GetComponentInChildren<ParticlesManager>();
        _timerRef = _timer;
        _amount = _amountRef;
    }

    private void Update()
    {
        if (Behaviour != null)
        Behaviour();
    }
    private void SummonSound()
    {
        //_spawnPosition.rotation= Quaternion.Euler(UnityEngine.Random.Range(-45.0f, 45.0f + 1), _startRotationY+ UnityEngine.Random.Range(45.0f, 135.0f + 1), 0);
        var Sound = Factory_CrashSound.Instance.Create();
        Sound.transform.position = transform.position + (_dir.position- transform.position).normalized * _distance;
        var script = Sound.GetComponent<Abstract_Sound>();
        Vector3 Dir = _dir.position + new Vector3(UnityEngine.Random.Range(-1.1f, 1.1f + 1), UnityEngine.Random.Range(-1.1f, 1.1f + 1), 0);
        Vector3 NewDir = (Dir - transform.position).normalized;
        script.ForceDirection(NewDir);
        script.Speed(5.0f);
        _particlesManager.PlayOnce();
        //AudioStorage.Instance.ZapSound();
    }

    public void Activate()
    {
        _timer = _timerRef;
        Behaviour = Counter;
    }
    public void DesActivate()
    {
        Behaviour -= Counter;
    }

    private void Counter()
    {
        _timer -= Time.deltaTime;

        if (_timer < 0)
        {
            _amount--;
            SummonSound();
            _timer = 0.5f;
            if (_amount <= 0)
            {
                _timer = _timerRef;
                _amount = _amountRef;
            }

        }
    }
    private void OnDestroy()
    {
        _room.ActRoom -= Activate;
        _room.DesActRoom -= DesActivate;
    }
}


