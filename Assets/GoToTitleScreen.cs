using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToTitleScreen : MonoBehaviour
{

    [SerializeField] private string _playerObjectName = "Player";
    [SerializeField] private string _menuSceneName = "MainMenu";

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == _playerObjectName)
        {

            SceneManager.LoadScene(_menuSceneName);
        }
    }
}
