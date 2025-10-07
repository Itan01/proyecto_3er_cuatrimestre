using UnityEngine;
public class Cons_CoughState: INoise
{
    private float _timer, _refTimer;
    public Cons_CoughState(float Timer)
    {
        _refTimer = Timer;
        _timer = _refTimer;
    }
    public void Noiser()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _timer = _refTimer;
            EventManager.Trigger(EEvents.Reset);// Kill Player
        }
    }
}

