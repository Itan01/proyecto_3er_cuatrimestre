using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAvatar : MonoBehaviour
{
    private PlayerManager _scriptManager;
    void Start()
    {
        _scriptManager = GetComponentInParent<PlayerManager>();
    }
    public void ActivateAreaSound()
    {
        _scriptManager.SetAreaCatching(true);
    }

    public void DesActivateAreaSound()
    {
        _scriptManager.SetAreaCatching(false);
      
    }
    public void PlayerCanNotShoot()
    {
        _scriptManager.PlayerCanShoot(false);
    }
    public void PlayerCanShoot()
    {
        _scriptManager.PlayerCanShoot(true);
    }
}