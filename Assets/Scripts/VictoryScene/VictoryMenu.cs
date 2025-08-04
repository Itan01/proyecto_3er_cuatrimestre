using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryMenu : MonoBehaviour
{
    private bool _anyButton = false;
    private int _time, _tries, _money;
    private Animator _animator;
    [SerializeField] private GameObject _scoreScene;
    [SerializeField] private GameObject _CongratulationScene;
    [SerializeField] private GameObject _rank;
    Action NextBehaviour;
    private void Start()
    {
        _rank.gameObject.SetActive(false);
        NextBehaviour = DoMaths;
        _animator=GetComponent<Animator>();
    }
    private void Update()
    {
        if (Input.anyKeyDown && _anyButton==false)
        {
            _anyButton = true;
            NextBehaviour();
        }
    }
    private void ShowCongratulations()
    {
        _CongratulationScene.SetActive(true);
        _scoreScene.SetActive(false);
    }

    private void DoMaths()
    {
        _rank.gameObject.SetActive(true);
        _animator.SetBool("Change", _anyButton);
        _animator.SetTrigger("ShowRank");
        _anyButton = false;
        SetLetter();
        NextBehaviour = ShowCongratulations;
    }

    public void SetTime(int value)
    {
        _time= value;
    }
    public void SetMoney(int value)
    {
        _money = value;
    }
    public void SetTries(int value)
    {
        _tries = value;
    }

    private void SetLetter()
    {
        int value = _money - ((_time/60) * 100)-_tries * 100;
        GetComponentInChildren<VictorySetRank>().SetRank(value);
    }
}
