using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetObjectAnimation : MonoBehaviour
{
    private Animator _animator;
    private ControlAnimator _controlAnimator;
    private bool _isPlaying;
    public string text = "TextInteract";
    public GameObject textInteract;
    private GameObject _currentTextInteract;
    void Start()
    {
        _animator = GetComponent<Animator>();
        _controlAnimator = new ControlAnimator(_animator);

        _currentTextInteract = GameObject.FindGameObjectWithTag(text);
        
        if (_currentTextInteract != null)
        {
            _currentTextInteract.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.TryGetComponent<PlayerManager>(out PlayerManager script))
        {
            _currentTextInteract.tag = text;

            if (!_isPlaying)
            {
                _isPlaying = true;
                _controlAnimator.SetBoolAnimator("Shine", _isPlaying);

                _currentTextInteract = Instantiate(textInteract, transform.position + Vector3.up * 2f, Quaternion.identity);

                Canvas mainCanvas = FindObjectOfType<Canvas>();
                if(mainCanvas != null && _currentTextInteract != null)
                {
                    _currentTextInteract.transform.SetParent(mainCanvas.transform, false);
                }
            }
        }
    }

    private void OnTriggerExit(Collider Player)
    {
        if (Player.TryGetComponent<PlayerManager>(out PlayerManager script))
        {
            _isPlaying = false;
            _controlAnimator.SetBoolAnimator("Shine", _isPlaying);

            if (textInteract != null && _currentTextInteract != null)
            {
                Destroy(_currentTextInteract);
                _currentTextInteract = null;
            }
       
            else if (_currentTextInteract != null)
            {
                _currentTextInteract.SetActive(false);
            }
        }
    }
}
