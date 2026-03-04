using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shorcuts : MonoBehaviour
{
    [SerializeField] private Transform[] _points;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            GameManager.Instance.PlayerReference.transform.position = _points[0].position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            GameManager.Instance.PlayerReference.transform.position = _points[1].position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            GameManager.Instance.PlayerReference.transform.position = _points[2].position;
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4) || Input.GetKeyDown(KeyCode.Keypad4))
        {

            GameManager.Instance.PlayerReference.transform.position = _points[3].position;
        }
    }
}
