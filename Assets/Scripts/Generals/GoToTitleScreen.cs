using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTitleScreen : MonoBehaviour
{
    [SerializeField] private string _menuSceneName = "MainMenu";

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.layer == 27)
        {
            Debug.Log("HII");
            SceneManager.LoadScene(_menuSceneName);
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
