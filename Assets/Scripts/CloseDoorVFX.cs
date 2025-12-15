using System.Collections;
using System.Collections.Generic;
using UnityEngine.VFX;
using UnityEngine;

public class CloseDoorVFX : MonoBehaviour
{
    [SerializeField] private VisualEffect _slamEffect;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySlam()
    {
        if(_slamEffect != null)
        {
        _slamEffect.Play();
        }
    }
}
