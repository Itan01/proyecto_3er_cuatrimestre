using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummonSoundFromDoor : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private bool _doorOpen=false, _forceDoor=false;
    [SerializeField] private GameObject _soundToSummon;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider Entity)
    {
        if (Entity.GetComponent<EntityMonobehaviour>() && !_forceDoor)
        {
            if(_doorOpen) return;
            if (Entity.TryGetComponent<PlayerManager>(out PlayerManager script))
            {
                if(script.GetCaptured())
                SummonSound(Entity.transform.position, true);
            }
                
            else
                SummonSound(Entity.transform.position, false);
            _doorOpen = true;
            _animator.SetBool("isOpen", _doorOpen);

        }
    }
    private void OnTriggerExit(Collider Entity)
    {
        if (Entity.GetComponent<EntityMonobehaviour>() && !_forceDoor)
        {
            if (!_doorOpen) return;
            _doorOpen = false;
            _animator.SetBool("isOpen", _doorOpen);
            if (Entity.GetComponent<PlayerManager>())
                SummonSound(Entity.transform.position, true);
            else
                SummonSound(Entity.transform.position, false);
        }
    }

    private void SummonSound(Vector3 EntityPosition, bool SummonedByPlayer)
    {
        Vector3 Orientation, SelfPosition, ModelPosition;
        ModelPosition = GetComponentInChildren<Transform>().position;
        SelfPosition = transform.position;
        SelfPosition.y += ModelPosition.y;
        var Sound = Instantiate(_soundToSummon, transform.position, Quaternion.identity);
        Orientation = (SelfPosition - EntityPosition).normalized;
        Orientation.y = 0;
        AbstractSound ScriptSound = Sound.GetComponent<AbstractSound>();
        ScriptSound.SetIfPlayerSummoned(SummonedByPlayer);
        ScriptSound.SetDirection(Orientation, Random.Range(3.0f, 7.0f + 1), 1.0f);
    }

    public void ForceDoorsClose(bool State)
    {
        _forceDoor = State;
        if (!_doorOpen) return;
        _animator.SetBool("isOpen", false);
    }
}
