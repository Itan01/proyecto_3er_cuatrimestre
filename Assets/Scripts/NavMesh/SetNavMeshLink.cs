using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;

public class SetNavMeshLink : MonoBehaviour
{
    private NavMeshLink _link;

    private void Start()
    {
        _link =GetComponentInChildren<NavMeshLink>();
    }

    public void SetLink(bool State)
    {
        _link.gameObject.SetActive(State);
    }
}
