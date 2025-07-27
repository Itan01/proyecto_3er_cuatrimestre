using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingScript : MonoBehaviour
{
    private Light _light;
    private void Awake()
    {
        GetComponentInParent<RoomManager>().ActRoom += ActivateLights;
        GetComponentInParent<RoomManager>().DesActRoom += DesActivateLights;
        _light=GetComponentInChildren<Light>();
        DesActivateLights();

    }
    public void ActivateLights()
    {
        AudioStorage.Instance.LightSwitch();
        _light.gameObject.SetActive(true);
    }
    public void DesActivateLights()
    {
        _light.gameObject.SetActive(false);
    }
}
