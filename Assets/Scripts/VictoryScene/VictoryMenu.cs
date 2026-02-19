using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryMenu : MonoBehaviour
{
    private bool _anyButton = false;
    [SerializeField] private int _time, _tries, _money;
    private Animator _animator;
    [SerializeField] private VictorySetRank _setRank;
    [SerializeField] private GameObject _scoreScene;
    [SerializeField] private GameObject _CongratulationScene;
    [SerializeField] private GameObject _rank;
    [SerializeField] private AudioSource _music;
    Action NextBehaviour;
    private void Start()
    {
        NextBehaviour = DoMaths;
        GameManager.Instance.LastScreen=EScreenName.VictoryMenu;
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
        _music.Play();
        _scoreScene.SetActive(false);
    }

    private void DoMaths()
    {
        SetLetter();
        _animator.SetBool("Change", _anyButton);
        _animator.SetTrigger("ShowRank");
        _anyButton = false;
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
        int value =( _money -_tries * 100);
        Debug.Log(value);
        _setRank.SetRank(value);
    }
}
