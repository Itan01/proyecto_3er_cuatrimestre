using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class interactableButton : MonoBehaviour, IInteractableObject
{
    [SerializeField] private GameObject _brokendoor;
    [SerializeField] private GameObject _textInteract;
    private string _text = "Press 'C' to Crouch";
    [SerializeField] private bool _playOnce = false, _check = false;
    private Animation _doorAnimation;


    private void Start()
    {
        _doorAnimation = GetComponentInChildren<Animation>();
    }   

    private void Update()
    {
        if (!_playOnce) return;
        if (_check)
            _textInteract.SetActive(true);
        else
            _textInteract.SetActive(false);
    }
    public void OnInteract()
    {
        if (!_playOnce)
        {
            _doorAnimation.Play();
            _playOnce = true;
            GetComponent<BoxCollider>().isTrigger = true;
            GetComponent<BoxCollider>().center = new Vector3(0.0f, 1.0f, -3.5f);
            GetComponent<BoxCollider>().size = new Vector3(4.0f, 8.0f, 5.0f);
            gameObject.layer = 0;
        }

    }

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _check = true;
            _textInteract.GetComponentInChildren<TMP_Text>().text = _text;
        }
    }

    private void OnTriggerExit(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            _check = false;
        }

    }
}
