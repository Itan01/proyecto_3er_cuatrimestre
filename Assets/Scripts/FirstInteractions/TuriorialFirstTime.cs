using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuriorialFirstTime : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.FirstTimeReference = this;
        gameObject.SetActive(false);
    }
}
