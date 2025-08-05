using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class FirstTimeInteraction : MonoBehaviour
{
    [SerializeField] private string _text = "Press any Button To do it";
    [SerializeField] private KeyCode _button, _secondButton;
    [SerializeField] private int _count = 0,_maxCount=3;
    private TuriorialFirstTime _showText;
    Action CheckInput;
    private void Start()
    {
        _showText=GameManager.Instance.FirstTimeReference;
    }
    private void Update()
    {
        if(CheckInput != null)
            CheckInput();
    }
    private void OnTriggerEnter(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _showText.gameObject.SetActive(true);
            _showText.GetComponentInChildren<TMP_Text>().text = _text;
            CheckInput += Input;
        }
    }

    private void OnTriggerExit(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _showText.gameObject.SetActive(false);
            CheckInput -= Input;
        }

    }
    private void Input()
    {
        if (UnityEngine.Input.GetKeyUp(_button) || UnityEngine.Input.GetKeyUp(_secondButton))
        {
            _count++;
            if(_count>= _maxCount)
            {
                gameObject.SetActive(false);
                _showText.gameObject.SetActive(false);
            }
        }
    }
}
