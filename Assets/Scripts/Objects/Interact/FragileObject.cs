using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.EventSystems.EventTrigger;

public class FragileObject : AbstractObjects, ISoundInteractions
{
    [SerializeField] private GameObject _sound;
    [SerializeField] private float _sizeMultiplier;
    private NavMeshObstacle _navObstacle;

    protected override void Start()
    {
        base.Start();
        _navObstacle = GetComponent<NavMeshObstacle>();
    }

    protected override void Update()
    {
        base.Update();
    }
    public float GetSize()
    {
        return _sizeMultiplier;
    }

    protected override void SetFeedback(bool State)
    {
        _animator.SetBool("Shine", State);
    }
    public void IIteraction(bool PlayerShootIt)
    {
        var Sound = Instantiate(_sound, transform.position, Quaternion.identity);
        Sound.GetComponent<SoundRadiusTrigger>().SetMultiplier(GetSize());
        Destroy(gameObject);
        AudioStorage.Instance.GlassBrokenSound();
        if (_navObstacle !=null)
        {
            _navObstacle.enabled = false;
        }

    }

}

