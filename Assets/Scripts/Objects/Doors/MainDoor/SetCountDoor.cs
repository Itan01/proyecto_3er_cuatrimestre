using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetCountDoor : MonoBehaviour
{
    [SerializeField]private TMP_Text _textMesh;
    private Cons_LockOnTarget _lockOnTarget;

    private void Awake()
    {
        _textMesh = GetComponent<TMP_Text>();
    }
    private void Start()
    {
        _lockOnTarget = new Cons_LockOnTarget(transform);
    }
    private void Update()
    {
        _lockOnTarget.Lock();
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
