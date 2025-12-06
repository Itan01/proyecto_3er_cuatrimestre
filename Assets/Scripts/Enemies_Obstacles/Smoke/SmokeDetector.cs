using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeDetector : MonoBehaviour
{
    [SerializeField]private float _timer = 0.0f, _refTimer = 2.0f;
    [SerializeField] private bool _isRunning = false;
    private void Start()
    {
        GetComponentInParent<RoomManager>().SetSmoke(gameObject);
        gameObject.SetActive(false);
    }
    void OnTriggerEnter(Collider Player)
    {
        if (Player.TryGetComponent(out EntityMonobehaviour Script))
        {
            StartCoroutine(Timer());

        }
    }

    void OnTriggerExit(Collider Player)
    {
        if (Player.TryGetComponent(out EntityMonobehaviour Script))
        {
            _isRunning = false;
            _timer = 0.0f;
        }
    }

    private IEnumerator Timer()
    {
        _isRunning=true;
        while (_isRunning)
        {
            _timer += Time.deltaTime;
            if (_timer > _refTimer)
            {
                EventManager.Trigger(EEvents.Reset);
                _isRunning = false;
            }
            yield return null;  
        }

        yield return null;
    }
}