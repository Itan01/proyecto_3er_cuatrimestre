using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class BaseDoor : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private bool _doorOpen=true;
    [SerializeField] private int _entities = 0;



    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _animator.SetBool("isOpen",_doorOpen);
    }

    private void OnTriggerEnter(Collider Entity)
    {
        if (Entity.GetComponent<EntityMonobehaviour>())
        {
            if(_doorOpen) return;
            if (Entity.TryGetComponent<PlayerManager>(out var script))
                if (script.IsCaptured) return;
            _doorOpen = true;
            _animator.SetBool("isOpen", _doorOpen);
            _entities++;
            AudioStorage.Instance.OpenDoorSound();
        }
    }
    
    private void OnTriggerExit(Collider Entity)
    {
        if (Entity.GetComponent<EntityMonobehaviour>())
        {
            if (!_doorOpen) return;
            _entities--;
            if(_entities <=0)
            {
                _entities = 0;
                _doorOpen = false;
                _animator.SetBool("isOpen", _doorOpen);
                AudioStorage.Instance.CloseDoorSound();
                
            }
        }
    }
}
