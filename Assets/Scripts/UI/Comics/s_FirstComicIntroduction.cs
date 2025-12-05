using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class s_FirstComicIntroduction : MonoBehaviour
{
    private s_UITextController _textScript;
    [SerializeField] private AudioSource _music;
    private Animator _animator;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _textScript = GetComponentInChildren<s_UITextController>();
        if (GameManager.Instance.FirstTimePlay == false)
        {
            StartRunning();
        }
        else
        {
            DesActivate();
        }
     }

    private void Update()
    {
        if (Input.anyKeyDown && !Input.GetKey(KeyCode.Escape))
            _animator.SetTrigger("Transition");
    }
    public void SetNewText(string Text) 
    {
        _textScript.SetText(Text);
    }
    public void StartRunning()
    {
        _music.Play();
        GameManager.Instance.FirstTimePlay=false;
        GameManager.Instance.PlayerReference.SetIfPlayerCanMove(true);
        UIManager.Instance.Timer.IsRunning(true);
        gameObject.SetActive(false);
    }
    private void DesActivate()
    {
        GameManager.Instance.PlayerReference.SetIfPlayerCanMove(false);
    }
}
