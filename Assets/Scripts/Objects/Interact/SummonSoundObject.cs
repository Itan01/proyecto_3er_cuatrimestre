using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSoundObject : MonoBehaviour
{
    [SerializeField] private GameObject _genericSound;
    private AbsStandardSoundMov _script;

    void OnCollisionEnter(Collision Sound)
    {
        if(Sound.gameObject.CompareTag("Sound"))
        {
            var Summoner = Instantiate(_genericSound, transform.position + new Vector3(0.0f,1.0f,0.0f) * 2, Quaternion.identity);
            _script = Summoner.GetComponent<AbsStandardSoundMov>();
            _script.SetDirection(Summoner.transform.position - transform.position, 5.0f, 1.0f);
         
            Destroy(gameObject);
        }
    }

}

