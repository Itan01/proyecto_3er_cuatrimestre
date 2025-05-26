using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class radiusToHearSound : MonoBehaviour
{
    private StandEnemyManager _scriptManager;
    private void Start()
    {
        _scriptManager = GetComponentInParent<StandEnemyManager>();
    }

    void OnTriggerEnter(Collider Entity)
    {
        if (Entity.TryGetComponent<AbstractSound>(out AbstractSound ScriptSound))
        {
            ScriptSound.SetTarget(transform, 7.5f);
        }
        if (Entity.TryGetComponent<PlayerManager>(out PlayerManager ScriptPlayer))
            _scriptManager.SetMode(1);

    }
}
