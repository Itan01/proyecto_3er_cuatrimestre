using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSoundFromWalking : MonoBehaviour
{
    [SerializeField] private GameObject _sound;
    private void OnTriggerEnter(Collider Entity)
    {
        if (Entity.TryGetComponent<EntityMonobehaviour>(out EntityMonobehaviour ScriptEntity))
        {
            //Debug.Log(Entity.name);
            ScriptEntity.SetSoundInvoker(true);
            ScriptEntity.GameObjectSoundInvoker(_sound);
        }
    }
    private void OnTriggerExit(Collider Entity)
    {
        if (Entity.TryGetComponent<EntityMonobehaviour>(out EntityMonobehaviour ScriptEntity))
        {
            ScriptEntity.SetSoundInvoker(false);
        }
    }
}
