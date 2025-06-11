using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UISetSound : MonoBehaviour
{
    [SerializeField] Sprite[] _sound;
    [SerializeField] ParticleSystem _particlesUI;
    private Image _uI;
    private Color _color;
    private void Start()
    {
        _uI = GetComponent<Image>();
        _color= GetComponent<Image>().color;
        _color.a = 0.5f;
        _uI.color = _color;
    }
    public void SetSound(int index)
    {
        _uI.sprite = _sound[index];
        if (index == 0)
        {
            _color.a = 0.5f;
            _uI.color = _color; 
            if(_particlesUI != null ) 
              _particlesUI.Play();
        }
        else
        {
            _color.a = 1.0f;
            _uI.color = _color;
        }
    }
}
