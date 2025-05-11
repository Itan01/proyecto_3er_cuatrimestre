using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISetSound : MonoBehaviour
{
    [SerializeField] Sprite[] _sound;
    private Image _uI;
    private void Start()
    {
        _uI = GetComponent<Image>();
    }
    public void SetSound(int index)
    {
        _uI.sprite = _sound[index];
    }
}
