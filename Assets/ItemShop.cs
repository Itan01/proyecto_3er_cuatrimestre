using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemShop : MonoBehaviour
{
    [SerializeField] private DATA_ITEMSHOP Data;
    [SerializeField] private Image _image;
    [SerializeField] private TextMeshProUGUI _textPrice;
    [SerializeField]private TextMeshProUGUI _textTittle;
    [SerializeField] private TextMeshProUGUI _textDescription;

    void Start()
    {
        _image.sprite = Data.Image;
        _textPrice.text = $"${Data.Price:000}";
        _textTittle.text = Data.Tittle;
        _textDescription.text = Data.Description;
    }   
    public int Price {  get { return Data.Price; } }
    public AudioClip Clip {  get { return Data.Clip; } }
}
