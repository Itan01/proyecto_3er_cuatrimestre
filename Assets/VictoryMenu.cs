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

    private void Update()
    {
        if (Input.anyKey && _anyButton==false)
        {
            _anyButton = true;
            DoMaths();
        }
    }
    private void ShowCongratulations()
    {
        _CongratulationScene.SetActive(true);
        _scoreScene.SetActive(false);
    }

    private void DoMaths()
    {
        _animator.SetBool("Change",true);
        ShowCongratulations();
    }

    public void SetTime(int value)
    {
        _time= value;
    }
    public void SetMoney(int value)
    {
        _time = value;
    }
    public void SetTrie(int value)
    {
        _time = value;
    }
}
