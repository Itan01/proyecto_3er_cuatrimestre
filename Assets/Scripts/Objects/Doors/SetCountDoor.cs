using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetCountDoor : MonoBehaviour
{
    private MainDoorManager _scriptParent;
    private TextMeshPro _textMesh;

    private void Start()
    {
        _scriptParent = GetComponentInParent<MainDoorManager>();
        _textMesh= GetComponent<TextMeshPro>();
    }

    public void SetValue(int ActualValue, int MaxValue)
    {
        _textMesh.text = $"{ActualValue}/{MaxValue}";
        if (ActualValue <= 0) 
        {
            Destroy(_textMesh.gameObject);
        }
    }
}
