using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class s_UITextController : MonoBehaviour
{
    private TMP_Text _text;

    private void Start()
    {
        _text = GetComponent<TMP_Text>();

    }
    public void SetText(string Text)
    {
        _text.text= Text;
    }
}
