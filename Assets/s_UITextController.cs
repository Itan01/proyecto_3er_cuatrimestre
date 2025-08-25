using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class s_UITextController : MonoBehaviour
{
    #region Values & Start
    private float _speed= 2f;
    private float _alpha = 0.0f;
    private TMP_Text _text;
    private bool _isFading =false;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();

    }
    #endregion
    public void SetText(string Text)
    {
        _text.text= Text;
    }
}
