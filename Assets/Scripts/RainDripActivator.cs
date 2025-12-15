using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class RainDripActivator : MonoBehaviour
{
    [SerializeField] private ScriptableRendererFeature _renderRainDripFullScreen;
    void OnTriggerEnter(Collider Entity)
    {
        if (Entity.GetComponent<PlayerManager>())
        {
            _renderRainDripFullScreen.SetActive(true);
        }
    }

    void OnTriggerExit(Collider Entity)
    {
        if (Entity.GetComponent<PlayerManager>())
        {
            _renderRainDripFullScreen.SetActive(false);

        }
    }
}
