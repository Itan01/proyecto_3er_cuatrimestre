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
            SceneManager.LoadScene(_menuSceneName);
        }
    }
}
