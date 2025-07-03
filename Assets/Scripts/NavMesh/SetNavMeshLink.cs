using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetNavMeshLink : MonoBehaviour
{
    private SetNavMeshLink _link;

    private void Start()
    {
        _link =GetComponentInChildren<SetNavMeshLink>();
    }

    public void SetLink(bool State)
    {
        gameObject.SetActive(State);
    }
}
