using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FragileObject : AbstractObjects
{
    [SerializeField] private GameObject _sound;
    private PlayerManager _player;

    protected override void Start()
    {
        base.Start();
        _player = GameManager.Instance.PlayerReference;

    }

    protected void Update()
    {
        if((_player.transform.position - transform.position).magnitude <= _distanceToAnimate)//Show Animation
            _animated=true;
        else
            _animated=false;
        SetAnimation(_animated);
    }
    void OnCollisionEnter(Collision Sound)
    {
        if(Sound.gameObject.GetComponent<AbstractSound>())
        {
            var Summoner = Instantiate(_sound, transform.position + new Vector3(0.0f,1.0f,0.0f), Quaternion.identity);
         
            Destroy(gameObject);
        }
    }
    private void SetAnimation(bool State)
    {
        _animator.SetBool("Shine", State);
    }
}

