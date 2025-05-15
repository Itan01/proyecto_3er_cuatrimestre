using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSoundFromDoor : MonoBehaviour
{
    private Animator _animator;
    [SerializeField] private GameObject _soundToSummon;
    private bool _forceClose;
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void OnTriggerEnter(Collider Entity)
    {
        if (Entity.TryGetComponent<EntityMonobehaviour>(out EntityMonobehaviour ScriptEntity)&& !_forceClose)
        {
                _animator.SetBool("isOpen", true);
                SummonSound(Entity.gameObject);

        }
    }
    private void OnTriggerExit(Collider Entity)
    {
        Debug.Log("Checkeando");
        if (Entity.TryGetComponent<EntityMonobehaviour>(out EntityMonobehaviour ScriptEntity))
        {
                _animator.SetBool("isOpen", false);
        }
    }

    private void SummonSound(GameObject Entity)
    {
        Vector3 _orientation, SelfPosition;
        SelfPosition = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
        var Sound = Instantiate(_soundToSummon, SelfPosition, Quaternion.identity);
        _orientation = (transform.position - Entity.transform.position).normalized + SelfPosition;
        AbstractSound ScriptSound = Sound.GetComponent<AbstractSound>();
        ScriptSound.SetDirection(_orientation, Random.Range(3.0f, 7.0f + 1), 1.0f);
    }
    public void SetAnimation(bool State, bool ForceClose)
    {
        _animator.SetBool("isOpen", State);
        _forceClose= ForceClose;
    }
}
