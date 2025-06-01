using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InteractableDoor : MonoBehaviour
{
    [SerializeField] private GameObject _door;
    [SerializeField] private bool _playOnce = false, _check = false;
    private Animator _doorAnimation;


    private void Start()
    {
        _doorAnimation = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
    }
    public void OnInteract()
    {
        if (!_playOnce)
        {
            _doorAnimation.SetBool("isOpen", true);
            _playOnce = true;
            gameObject.layer = 0;
        }

    }
}
