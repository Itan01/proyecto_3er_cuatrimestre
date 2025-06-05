using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonMessageSound : MonoBehaviour
{
    [SerializeField] private GameObject _soundMessage;
    [SerializeField] private float _timer, _timerRef;

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            var Sound = Instantiate(_soundMessage, transform.position,Quaternion.identity);
            AbstractSound script= Sound.GetComponent<AbstractSound>();
            script.SetDirection(transform.forward, 3.0f,1.0f);
            _timer = _timerRef;
        }
    }
}
