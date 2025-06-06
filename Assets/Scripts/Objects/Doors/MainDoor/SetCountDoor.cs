using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetCountDoor : MonoBehaviour
{
    [SerializeField]private TMP_Text _textMesh;

    private void Awake()
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
