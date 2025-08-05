using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTitleScreen : MonoBehaviour
{
    [SerializeField] private CountdownTimer _timerScript;
    [SerializeField] private string _menuSceneName = "MainMenu";

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _timerScript.StopTimerUI();
            StartCoroutine(ChargeScene());
        }
    }

    private IEnumerator ChargeScene()
    {
        yield return new WaitForSeconds(1.0f);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(_menuSceneName);

    }
}
