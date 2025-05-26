using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FirstTimeInteraction : MonoBehaviour
{
    private Animator _animator;
    private ControllerAnimator _controlAnimator;
    private bool _isPlaying;
    private string _text = "Press 'E' To Interact";
    [SerializeField] private GameObject _textInteract;
    [SerializeField] private bool _check = false;
    [SerializeField] private Transform _transformPlayer=null;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _controlAnimator = new ControllerAnimator(_animator);
    }

    private void Update()
    {
        if (_check)
        {
            if ((_transformPlayer.transform.position - transform.position).magnitude <= 4.0f)
                _textInteract.SetActive(true);
            else
                _textInteract.SetActive(false);
        }

    }
    private void OnTriggerEnter(Collider Player)
    {
        if (Player.TryGetComponent<PlayerManager>(out PlayerManager script))
        {
            _check = true;
            _transformPlayer =script.transform;
            _textInteract.GetComponentInChildren<TMP_Text>().text = _text;

            if (!_isPlaying)
            {
                _isPlaying = true;
                _controlAnimator.SetBoolAnimator("Shine", _isPlaying);
            }
        }
    }

    private void OnTriggerExit(Collider Player)
    {
        if (Player.TryGetComponent<PlayerManager>(out PlayerManager script))
        {
            _check = false;
            _isPlaying = false;
            _controlAnimator.SetBoolAnimator("Shine", _isPlaying);
        }

    }   
}
