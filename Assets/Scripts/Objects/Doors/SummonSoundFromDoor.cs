using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SummonSoundFromDoor : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private bool _doorOpen=false, _forceDoor=false;
    //[SerializeField] private GameObject _soundToSummon;
    [SerializeField] private int _count = 0;
    [SerializeField] private AudioClip _openSound, _closeSound;
    [SerializeField] private float _soundVolume= 0.80f;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider Entity)
    {
        if (Entity.GetComponent<EntityMonobehaviour>() && !_forceDoor)
        {
            if(_doorOpen) return;
            if (Entity.GetComponent<PlayerManager>().GetCaptured()) return;
                //SummonSound(Entity.transform.position, false);
            _doorOpen = true;
            _animator.SetBool("isOpen", _doorOpen);
            _count++;
            AudioManager.Instance.PlaySFX(_openSound, _soundVolume);

        }
    }
    private void OnTriggerExit(Collider Entity)
    {
        if (Entity.GetComponent<EntityMonobehaviour>() && !_forceDoor)
        {
            if (!_doorOpen) return;
            _count--;
            if(_count <0)
            {
                _count = 0;
                _doorOpen = false;
                _animator.SetBool("isOpen", _doorOpen);
                AudioManager.Instance.PlaySFX(_closeSound, _soundVolume);
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

    public void ForceDoorsClose(bool State)
    {
        _forceDoor = State;
        if (!_doorOpen) return;
        _animator.SetBool("isOpen", false);
        
    }
}
