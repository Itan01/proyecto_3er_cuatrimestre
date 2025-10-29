using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSoundFromWalking : MonoBehaviour
{
    private float _time=0.5f;
    private bool _isPlayer=false;
    private void OnTriggerEnter(Collider Entity)
    {
        if (Entity.TryGetComponent<EntityMonobehaviour>(out EntityMonobehaviour ScriptEntity))
        {
            if(ScriptEntity.GetComponent<PlayerManager>())
                _isPlayer = true;
            ScriptEntity.AddBehaviour(Noise);
            //Debug.Log(Entity.name);
            //ScriptEntity.SetSoundInvoker(true);
           // ScriptEntity.GameObjectSoundInvoker(_sound);
        }
    }
    private void OnTriggerExit(Collider Entity)
    {
        if (Entity.TryGetComponent<EntityMonobehaviour>(out EntityMonobehaviour ScriptEntity))
        {
            ScriptEntity.RemoveBehaviour(Noise);
            _isPlayer = false;
            // ScriptEntity.SetSoundInvoker(false);
        }
    }
    private void Noise()
    {
        _time -= Time.deltaTime;
        if(_time <= 0)
        {
            _time = 0.5f;
            var x=  Factory_CrashSound.Instance.Create();
            x.transform.position = transform.position + transform.up;
            x.transform.position += new Vector3(Random.Range(-1.1f,1.1f+1),0, Random.Range(-1.1f, 1.1f + 1));
            x.ForceDirection((x.transform.position - transform.position).normalized);
            x.Speed(2.0f);
            if (_isPlayer)
                x.ShootByPlayer=true;
        }
    }
}
