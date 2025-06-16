using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class FragileObject : AbstractObjects
{
    [SerializeField] private GameObject _sound;
    [SerializeField] private float _sizeMultiplier;

    protected override void Start()
    {
        base.Start();

    }

    protected override void Update()
    {
        base.Update();
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 28)
        {
            var Sound = Instantiate(_sound, transform.position, Quaternion.identity);
            Sound.GetComponent<SoundRadiusTrigger>().SetMultiplier(GetSize());
            Destroy(gameObject);
        }
    }
    public float GetSize()
    {
        return _sizeMultiplier;
    }

    protected override void SetFeedback(bool State)
    {
        _animator.SetBool("Shine", State);
    }
}

