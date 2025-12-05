using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class GlassNavMesh : AbstractObjects, ISoundInteractions
{
    private NavMeshLink _link;
    [SerializeField] private float _sizeMultiplier;
    protected override void Start()
    {
        base.Start();
        _link = GetComponentInChildren<NavMeshLink>();
        _link.gameObject.SetActive(false);
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
        var x = Factory_CrashSound.Instance.Create();
        x.transform.position = transform.position;
        x.ForceDirection(new Vector3(Random.Range(-1,1), Random.Range(-1, 1), Random.Range(-1, 1)));
        AudioStorage.Instance.GlassBrokenSound();
        DesactivateObject();
        _link.gameObject.SetActive(true);
    }

}
