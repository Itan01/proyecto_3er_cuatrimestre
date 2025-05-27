using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class SummonSoundFromDoor : MonoBehaviour
{
    private Animator _animator;
    private bool _doorOpen=false;
    [SerializeField] private GameObject _soundToSummon;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider Entity)
    {
        if (Entity.GetComponent<EntityMonobehaviour>())
        {
            if(_doorOpen) return;
            _doorOpen = true;
            _animator.SetBool("isOpen", _doorOpen);
            SummonSound(Entity.transform.position);
        }
    }
    private void OnTriggerExit(Collider Entity)
    {
        if (Entity.GetComponent<EntityMonobehaviour>())
        {
            if (!_doorOpen) return;
            _doorOpen = false;
            _animator.SetBool("isOpen", _doorOpen);
            SummonSound(Entity.transform.position);
        }
    }

    private void SummonSound(Vector3 EntityPosition)
    {
        Vector3 Orientation, SelfPosition, ModelPosition;
        ModelPosition = GetComponentInChildren<Transform>().position;
        SelfPosition = transform.position;
        SelfPosition.y += ModelPosition.y / 2;
        var Sound = Instantiate(_soundToSummon, SelfPosition, Quaternion.identity);
        Orientation = (SelfPosition - EntityPosition).normalized;
        Orientation.y = 0;
        AbstractSound ScriptSound = Sound.GetComponent<AbstractSound>();
        ScriptSound.SetDirection(Orientation, Random.Range(3.0f, 7.0f + 1), 1.0f);
    }
}
