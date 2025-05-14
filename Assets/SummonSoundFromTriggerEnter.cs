using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonSoundFromTriggerEnter : MonoBehaviour
{
    [SerializeField] private GameObject _sound;
    private AbstractSound _script;
    private PlayerManager _scriptPlayer;
    private bool _isActive = true;


    void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.GetComponent<PlayerManager>() != null && _isActive==true)
        {
            var Summoner = Instantiate(_sound, transform.position + new Vector3(0.0f, 1.0f, 0.0f), Quaternion.identity);
            _script = Summoner.GetComponent<AbstractSound>();
            _script.SetDirection(Summoner.transform.position - transform.position, 5.0f, 1.0f);

            _isActive = false;
        }
    }
}
