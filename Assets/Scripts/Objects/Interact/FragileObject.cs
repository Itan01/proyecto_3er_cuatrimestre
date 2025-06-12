using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class FragileObject : AbstractObjects
{
    [SerializeField] private GameObject _sound;
    [SerializeField] private float _sizeMultiplier;
    private PlayerManager _player;

    protected override void Start()
    {
        base.Start();
        _player = GameManager.Instance.PlayerReference;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 28)
        {
            Debug.Log("HII");
            var Sound = Instantiate(_sound,transform.position, Quaternion.identity);
            Sound.GetComponent<SoundRadiusTrigger>().SetMultiplier(GetSize());
            Destroy(gameObject);
        }
    }

    protected void Update()
    {
        if((_player.transform.position - transform.position).magnitude <= _distanceToAnimate)//Show Animation
            _animated=true;
        else
            _animated=false;
        SetAnimation(_animated);
    }
    public float GetSize()
    {
        return _sizeMultiplier;
    }
    private void SetAnimation(bool State)
    {
        _animator.SetBool("Shine", State);
    }
}

