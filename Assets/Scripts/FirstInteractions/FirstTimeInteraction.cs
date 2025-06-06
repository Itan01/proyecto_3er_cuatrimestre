using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirstTimeInteraction : MonoBehaviour
{
    [SerializeField] private string _text = "Press any Button To do it";
    private TuriorialFirstTime _showText;

    private void Start()
    {
        _showText=GameManager.Instance.FirstTimeReference;
    }
    private void OnTriggerEnter(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _showText.gameObject.SetActive(true);
            _showText.GetComponentInChildren<TMP_Text>().text = _text;
        }
    }

    private void OnTriggerExit(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _showText.gameObject.SetActive(false);
        }

    }
}
