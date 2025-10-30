using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_Crash : Abstract_Sound
{
    private ObjectPool<Sound_Crash> _pool;
    protected override void Start()
    {
        base.Start();
        _indexRef = ESounds.Crash;
    }

    // Update is called once per frame
    protected override void Update()
    {
    }
    public void Initialize(ObjectPool<Sound_Crash> Pool)
    {
        _pool = Pool;
    }

    public override void Refresh()
    {
        _playerShooted = false;
        _pool.Release(this);
    }
    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }
    protected override void LateUpdate()
    {
        base.LateUpdate();
    }
    private void OnCollisionEnter(Collision Entity)
    {
        if(Entity.gameObject.TryGetComponent<ISoundInteractions>(out var Script))
        {
            Script.IIteraction(_playerShooted);
        }
        if (_playerShooted)
        {
            var x = Factory_Explosion_Crash_Sound.Instance.Create();
            x.transform.position = transform.position;

        }
        Refresh();
    }
}
