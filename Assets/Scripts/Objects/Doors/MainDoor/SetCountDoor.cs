using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetCountDoor : MonoBehaviour
{
    private TMP_Text _textMesh;

    private void Start()
    {
        _textMesh= GetComponent<TMP_Text>();
    }

    public void SetValue(int ActualValue, int MaxValue)
    {
        _textMesh.text = $"{ActualValue}/{MaxValue}";
        if (ActualValue <= 0) 
        {
            gameObject.SetActive(false);
        }
    }
}
