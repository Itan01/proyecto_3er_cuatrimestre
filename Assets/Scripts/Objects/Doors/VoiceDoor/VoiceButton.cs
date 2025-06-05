using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VoiceButton : MonoBehaviour
{
    private VoiceDoorManager _doorManager;
    [SerializeField] protected ParticleSystem[] _particles;
    private  void Start()
    {
        _doorManager = GetComponentInParent<VoiceDoorManager>();
    }
    private void OnTriggerEnter(Collider Sound)
    {
        if (Sound.TryGetComponent<MessageSound>(out MessageSound ScriptSound))
        {
            _doorManager.CheckStatus();
            for (int i = 0; i < _particles.Length; i++)
                _particles[i].Play();
            Destroy(ScriptSound.gameObject);
        }
    }

    public void OnInteract()
    {
        Debug.Log("Wrong VoiceCode");
    }
}
