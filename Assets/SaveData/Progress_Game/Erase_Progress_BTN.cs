using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Erase_Progress_BTN : MonoBehaviour
{
    public void Erase()
    {
        Save_Progress_Json.Instance.Erase();
        new Reset_Progress();
    }
}
