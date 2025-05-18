using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetObjectAnimation : MonoBehaviour
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
            Debug.Log((_transformPlayer.transform.position - transform.position).magnitude);
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
    private void OnDestroy()
    {
        _textInteract.SetActive(false); 
    }

    //void Start()
    //{
    //    _animator = GetComponent<Animator>();
    //    _controlAnimator = new ControlAnimator(_animator);

    //    _current_textInteract = GameObject.FindGameObjectWithTag(text);

    //    if (_current_textInteract != null)
    //    {
    //        _current_textInteract.SetActive(false);
    //    }
    //}

    //private void OnTriggerEnter(Collider Player)
    //{
    //    if (Player.TryGetComponent<PlayerManager>(out PlayerManager script))
    //    {
    //        _current_textInteract.tag = text;

    //        if (!_isPlaying)
    //        {
    //            _isPlaying = true;
    //            _controlAnimator.SetBoolAnimator("Shine", _isPlaying);

    //            _current_textInteract = Instantiate(_textInteract, transform.position + Vector3.up * 2f, Quaternion.identity);

    //            Canvas mainCanvas = FindObjectOfType<Canvas>();
    //            if (mainCanvas != null && _current_textInteract != null)
    //            {
    //                _current_textInteract.transform.SetParent(mainCanvas.transform, false);
    //            }
    //        }
    //    }
    //}

    //private void OnTriggerExit(Collider Player)
    //{
    //    if (Player.TryGetComponent<PlayerManager>(out PlayerManager script))
    //    {
    //        _isPlaying = false;
    //        _controlAnimator.SetBoolAnimator("Shine", _isPlaying);

    //        if (_textInteract != null && _current_textInteract != null)
    //        {
    //            Destroy(_current_textInteract);
    //            _current_textInteract = null;
    //        }

    //        else if (_current_textInteract != null)
    //        {
    //            _current_textInteract.SetActive(false);
    //        }
    //    }
    //}
}
