using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BaseDoor : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private bool _doorOpen=true, _forceDoor=false;
    [SerializeField] private int _count = 0;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        GetComponentInParent<RoomManager>().DetPlayer += ForceCallDoors;
        GetComponentInParent<RoomManager>().ResetDet += ForceOpenDoors;
        _animator.SetBool("isOpen",_doorOpen);
    }

    private void OnTriggerEnter(Collider Entity)
    {
        if (Entity.GetComponent<EntityMonobehaviour>() && !_forceDoor)
        {
            if(_doorOpen) return;
            if (Entity.TryGetComponent<PlayerManager>(out var script))
                if (script.GetCaptured()) return;
            _doorOpen = true;
            _animator.SetBool("isOpen", _doorOpen);
            _count++;
            AudioStorage.Instance.OpenDoorSound();
        }
    }
    private void OnTriggerExit(Collider Entity)
    {
        if (Entity.GetComponent<EntityMonobehaviour>() && !_forceDoor)
        {
            if (!_doorOpen) return;
            _count--;
            if(_count <=0)
            {
                _count = 0;
                _doorOpen = false;
                _animator.SetBool("isOpen", _doorOpen);
                AudioStorage.Instance.CloseDoorSound();
                //if (Entity.GetComponent<PlayerManager>())
                //    SummonSound(Entity.transform.position, true);
                //else
                //   SummonSound(Entity.transform.position, false);
            }

        }
    }

    private void SummonSound(Vector3 EntityPosition, bool SummonedByPlayer)
    {
        Vector3 Orientation, SelfPosition, ModelPosition;
        ModelPosition = GetComponentInChildren<Transform>().position;
        SelfPosition = transform.position;
        SelfPosition.y += ModelPosition.y;
       // var Sound = Instantiate(_soundToSummon, transform.position, Quaternion.identity);
        Orientation = (SelfPosition - EntityPosition).normalized;
        Orientation.y = 0;
       // AbstractSound ScriptSound = Sound.GetComponent<AbstractSound>();
      //  ScriptSound.SetIfPlayerSummoned(SummonedByPlayer);
       //  ScriptSound.SetDirection(Orientation, Random.Range(3.0f, 7.0f + 1), 1.0f);
    }

    public void ForceCloseDoors(bool state)
    {
        if (_forceDoor == state) return;

        _forceDoor = state;
        _doorOpen = !state;
        _animator.SetBool("isOpen", _doorOpen);

        if (_forceDoor)
        {
            AudioStorage.Instance.CloseDoorSound();
        }
        else
        {
            AudioStorage.Instance.OpenDoorSound();
        }
    }

    public void ForceCallDoors()
    {
        ForceCloseDoors(true);
    }
    public void ForceOpenDoors()
    {
        ForceCloseDoors(false);
    }
}
