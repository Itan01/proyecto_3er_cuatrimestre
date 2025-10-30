using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSoundFromWalking : MonoBehaviour
{
    private void OnTriggerEnter(Collider Entity)
    {
        if (Entity.TryGetComponent<EntityMonobehaviour>(out EntityMonobehaviour ScriptEntity))
        {
             ScriptEntity.AddNoise();
        }
    }
    private void OnTriggerExit(Collider Entity)
    {
        if (Entity.TryGetComponent<EntityMonobehaviour>(out EntityMonobehaviour ScriptEntity))
        {
            ScriptEntity.RemoveNoise();
        }
    }
}
