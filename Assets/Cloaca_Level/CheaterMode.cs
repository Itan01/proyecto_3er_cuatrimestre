using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheaterMode : MonoBehaviour
{
    [SerializeField]private Transform[] _points;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.Instance.PlayerReference.transform.position= _points[0].position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.Instance.PlayerReference.transform.position = _points[1].position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            GameManager.Instance.PlayerReference.transform.position = _points[2].position;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            GameManager.Instance.PlayerReference.transform.position = _points[3].position;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
