using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_FinishLevel : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        UIManager.Instance.FinishLevel();
    }

}
