using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyShop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _text;
    void Start()
    {
        if(_text == null) _text = GetComponentInChildren<TextMeshProUGUI>();
        UpdateValue();
    }

    public void UpdateValue() 
    {
        _text.text = $"${Save_Progress_Json.Instance.Data.Money:000}";
    }   
}
