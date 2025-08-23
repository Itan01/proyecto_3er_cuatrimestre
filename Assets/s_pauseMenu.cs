using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_pauseMenu : MonoBehaviour
{
    private void Start()
    {
        SetActivate(true);
        UIManager.Instance.MenuPause = this;
        SetActivate(false);
    }

    public void SetActivate(bool State)
    {
        gameObject.SetActive(State);
    }
}
