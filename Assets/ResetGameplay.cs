using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetGameplay : MonoBehaviour
{
    private bool _playOnce;
    private void Start()
    {
        EventManager.Subscribe(EEvents.Reset, ResetGame);

    }
    private void ResetGame(params object[] Pararmeters)
    {
        StartCoroutine(Resseting());
    }
    private IEnumerator Resseting()
    {
        yield return new WaitForSeconds(1.5f);
        EventManager.Trigger(EEvents.ReStart);
    }
}
