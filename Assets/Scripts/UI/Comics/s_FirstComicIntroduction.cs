using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class s_FirstComicIntroduction : MonoBehaviour
{
    private s_UITextController _textScript;
    [SerializeField]private GameObject _audioSource;
    private Animator _animator;
    private bool _isTransitioning, _ending=false;
    private void Start()
    {
        _animator = GetComponent<Animator>();
        _textScript = GetComponentInChildren<s_UITextController>();
        if (!GameManager.Instance.FirstTimePlay)
        {
            GameManager.Instance.PlayerReference.SetIfPlayerCanMove(true,false);
            _audioSource.SetActive(true);
            gameObject.SetActive(false);
        }
        else
        {
            GameManager.Instance.PlayerReference.SetIfPlayerCanMove(false, false);
        }

     }

    private void Update()
    {
        if (Input.anyKeyDown && !_isTransitioning && !Input.GetKey(KeyCode.Escape))
            Transition();
    }
    private void Transition()
    {
        if (_ending)
        {
            _animator.SetTrigger("Ending");
        }
        _isTransitioning = true;
        _animator.SetTrigger("Transition");
    }
    public void StopTransition()
    {
        _isTransitioning = false;
    }
    public void SetNewText(string Text) 
    {
        _textScript.SetText(Text);
    }
    public void LastTransition()
    {
        _isTransitioning = false;
        _ending = true;
    }
    public void Desactivate()
    {
        _audioSource.SetActive(true);
GameManager.Instance.FirstTimePlay=false;
        GameManager.Instance.PlayerReference.SetIfPlayerCanMove(true, false);
        UIManager.Instance.Timer.IsRunning(true);
        gameObject.SetActive(false);
    }
}
