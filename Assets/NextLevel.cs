using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    private void Start()
    {
        if(GameManager.Instance.LastScreen==EScreenName.MainMenu)
           gameObject.SetActive(false);
    }
}
