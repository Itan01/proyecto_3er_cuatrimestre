using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSoundFromDoor : MonoBehaviour
{
    private Animation _animator;
    [SerializeField] private GameObject _soundToSummon;
    void Start()
    {
        _animator = GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider Entity)
    {
        if (Entity.TryGetComponent<EntityMonobehaviour>(out EntityMonobehaviour ScriptEntity))
        {
            if(_animator.isPlaying) return;
            _animator.Play();
            Vector3 _orientation, SelfPosition;
            SelfPosition = transform.position + new Vector3(0.0f, 1.0f, 0.0f);  
            var Sound = Instantiate(_soundToSummon, SelfPosition, Quaternion.identity);
            _orientation = (transform.position - Entity.transform.position).normalized + SelfPosition;
            AbstractSound ScriptSound= Sound.GetComponent<AbstractSound>();
            ScriptSound.SetDirection(_orientation, Random.Range(3.0f,7.0f +1), 1.0f);
        }
    }
}
