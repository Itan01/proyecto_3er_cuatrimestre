using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class DoorButton : MonoBehaviour, IInteractableObject
{
    private MainDoorManager _doorManager;
    void Start()
    {
        _doorManager = GetComponentInParent<MainDoorManager>();
    }
    private void OnTriggerEnter(Collider Sound)
    {
        if (Sound.TryGetComponent<AbstractSound>(out AbstractSound ScriptSound))
        {
            _doorManager.CheckStatus();
            Destroy(ScriptSound.gameObject);
        }
    }

    public void OnInteract()
    {
        Debug.Log("Wrong VoiceCode");
    }
}
