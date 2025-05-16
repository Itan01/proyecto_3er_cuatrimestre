using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        if (Entity.TryGetComponent<EntityMonobehaviour>(out EntityMonobehaviour ScriptEntity))
        {
            if(_doorOpen) return;
            _doorOpen = true;
            _animator.SetBool("isOpen", _doorOpen);
            Vector3 _orientation, SelfPosition;
            SelfPosition = transform.position + new Vector3(0.0f, 1.0f, 0.0f);  
            var Sound = Instantiate(_soundToSummon, SelfPosition, Quaternion.identity);
            _orientation = (transform.position - Entity.transform.position).normalized + SelfPosition;
            AbstractSound ScriptSound= Sound.GetComponent<AbstractSound>();
            ScriptSound.SetDirection(_orientation, Random.Range(3.0f,7.0f +1), 1.0f);
        }
    }
    private void OnTriggerExit(Collider Entity)
    {
        if (Entity.TryGetComponent<EntityMonobehaviour>(out EntityMonobehaviour ScriptEntity))
        {
            if (!_doorOpen) return;
            _doorOpen = false;
            _animator.SetBool("isOpen", _doorOpen);
        }
    }
}
