using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSummoner : MonoBehaviour
{
    [SerializeField] private float _timer;
    private float _timerRef;
    private int _amount;
    [Range(1, 5)][SerializeField] private int _amountRef;
    [SerializeField] float _startRotationY=0;
    [SerializeField] private Transform _spawnPosition;
    [SerializeField] private GameObject _sound;

    private void Start()
    {
        _timerRef = _timer;
        _amount = _amountRef;
    }

    private void Update()
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
    private void SummonSound()
    {
        _spawnPosition.rotation= Quaternion.Euler(Random.Range(-45.0f, 45.0f + 1), _startRotationY+ Random.Range(45.0f, 135.0f + 1), 0);
        var Sound = Instantiate(_sound, _spawnPosition.position, Quaternion.identity);
        AbstractSound script = Sound.GetComponent<AbstractSound>();
        script.SetDirection(_spawnPosition.forward, 5.0f, 1.0f);
    }
}


