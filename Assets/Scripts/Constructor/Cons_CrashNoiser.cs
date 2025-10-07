using UnityEngine;

public class Cons_CrashNoiser : INoise
{
    private float _timer, _refTimer;
    private EntityMonobehaviour _entity;

    public Cons_CrashNoiser(float Timer=0.5f)
    {
        _entity = null;
        _refTimer= Timer;
        _timer = _refTimer;
    }
    public Cons_CrashNoiser SetEntity(EntityMonobehaviour Entity)
    {
        _entity = Entity;
        return this;
    }
    public void Noiser()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _timer = _refTimer;
            MakeNoise();
        }
    }

    private void MakeNoise()
    {
        Vector3 RandomPosition = new Vector3(Random.Range(-2.0f, 1.0f + 1), 1.0f,Random.Range(-2.0f, 1.0f + 1)).normalized * 2;
        RandomPosition += _entity.transform.position;
        Vector3 Orientation = RandomPosition - _entity.transform.position;
        //CreateSound
    }

}
