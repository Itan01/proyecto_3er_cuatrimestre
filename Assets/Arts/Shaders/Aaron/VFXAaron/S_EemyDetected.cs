using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class S_EemyDetected : MonoBehaviour
{
    [Header("Behaviour")]
    public float _activationTime = 0.5f;

    public VisualEffect detectionVFX;

    private bool isAnimating = false;
    private static readonly int EventID = Shader.PropertyToID("OnPlay");

    void Start()
    {
        detectionVFX = GetComponent<VisualEffect>();
    }

    public void VFXOn()
    {
        if (isAnimating) return;

        detectionVFX.SendEvent(EventID);

        StartCoroutine(AnimateVFXParameters(true));
    }

    public void VFXOff()
    {
        if (isAnimating) return;

       detectionVFX.SendEvent(EventID);

        StartCoroutine(AnimateVFXParameters(false));
    }

    IEnumerator AnimateVFXParameters(bool activating)
    {
        isAnimating = true;
        yield return new WaitForSeconds(_activationTime); 
        isAnimating = false;
    }
}
