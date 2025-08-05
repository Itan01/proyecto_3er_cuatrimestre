using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DashCooldown : MonoBehaviour
{
    [SerializeField] private Image _cooldownCircleBar;
    [SerializeField] private PlayerDash _sprintScript;

    void Update()
    {
        if (_sprintScript.!_canDash)
        {
            float fill = 1f - (_sprintScript.Cooldown / _sprintScript.MaxCooldown);
            _cooldownCircleBar.fillAmount = fill;
        }
        else
        {
            _cooldownCircleBar.fillAmount = 1f; // Full when not cooling down
        }
    }
}
