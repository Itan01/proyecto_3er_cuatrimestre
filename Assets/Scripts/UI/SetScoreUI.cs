using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetScoreUI : MonoBehaviour
{
    private TMP_Text _reference;
    private void Start()
    {
        _reference = GetComponent<TMP_Text>();
        UIManager.Instance.TextReference = _reference;
        _reference.text = "$0";
    }
}
