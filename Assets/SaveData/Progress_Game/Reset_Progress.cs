using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reset_Progress
{
    public Reset_Progress()
    {
        Save_Progress_Json.Instance.Data.ApplyData();
        Save_Progress_Json.Instance.SaveData();
    }
}
