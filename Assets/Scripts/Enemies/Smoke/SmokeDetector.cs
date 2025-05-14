using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeDetector : MonoBehaviour
{
    private PlayerSmoke _scriptPlayer;
    void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.CompareTag("Player"))
        {
            _scriptPlayer = Player.GetComponent<PlayerSmoke>();
            _scriptPlayer.ToggleState(true);
        }
    }

    void OnTriggerExit(Collider Player)
    {
    if (Player.gameObject.CompareTag("Player"))
        {
            _scriptPlayer = Player.GetComponent<PlayerSmoke>();
            _scriptPlayer.ToggleState(false);
        }
    }
}