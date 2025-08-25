using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class s_UI_comicImage : MonoBehaviour
{
    [SerializeField] private Sprite[] _sprites;
    private s_FirstComicIntroduction _mainScript;
    private bool _transition=true;
    private Image _image;
    [SerializeField] private int _spriteIndex=0;
    private void Start()
    {
        _mainScript = GetComponentInParent<s_FirstComicIntroduction>();
        _image =GetComponent<Image>();
        StartCoroutine(FirstImage());
    }
    private void Update()
    {
        if (Input.anyKey && !_transition)
        {

            ShowNextImage();
        }
    }

    private void ShowNextImage()
    {
        if(_spriteIndex +1 < _sprites.Length)
        {

            _transition = true;
            StartCoroutine(FadeIN());
        }
        else
        {
            _transition = true;
            _mainScript.Desactivate();
        }
    }
    private IEnumerator FadeIN()
    {
        Color opacty = _image.color;
        opacty.a = 0.0f;
        _spriteIndex++;
        yield return new WaitForSeconds(0.25f);
        _image.sprite = _sprites[_spriteIndex];
        while (opacty.a < 1)
        {
            opacty.a += Time.deltaTime;
            _image.color = opacty;
            if (Input.anyKeyDown)
                opacty.a = 1.0f;
            yield return null;
        }
        //_mainScript.SetSprite(_sprites[_spriteIndex]);
        if (_spriteIndex  >= _sprites.Length - 1)
        {
            opacty.a = 0.0f;
            _image.color = opacty;
        }
        _transition = false;    
    }
    private IEnumerator FirstImage()
    {
        yield return new WaitForSeconds(0.25f);
        Color opacty = _image.color;
        while (opacty.a < 1) 
        {
            opacty.a += Time.deltaTime;
            _image.color = opacty;
            if (Input.anyKeyDown)
                opacty.a = 1.0f;
            yield return null;
        }
       // _mainScript.FirstSprite(_sprites[_spriteIndex]);
        _transition = false;
    }
}

