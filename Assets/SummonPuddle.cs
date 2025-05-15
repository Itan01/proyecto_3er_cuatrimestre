using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonPuddle : MonoBehaviour
{
    [SerializeField] private GameObject _reference;
    [SerializeField] private float _timer=0.0f, _refTimer=3.0f;
    void Start()
    {
        
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _refTimer)
            SummonSound();
    }
    private void SummonSound()
    {
        Vector3 randomDirection = -transform.up+ new Vector3(Random.Range(-0.1f,0.1f+1),0.0f, Random.Range(-0.1f, 0.1f + 1));
        var sound = Instantiate(_reference, transform.position,Quaternion.identity);
        sound.GetComponent<AbstractSound>().SetDirection(randomDirection, 1.0f,1.25f);
        _timer = 0.0f;
    }
}
