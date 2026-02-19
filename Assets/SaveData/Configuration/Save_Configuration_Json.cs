using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class Save_Configuration_Json : Abstract_SaveData
{
    public SaveData_Configuration Data;
    #region Singleton
    public static Save_Configuration_Json Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
#if UNITY_EDITOR
        _basePath = Application.dataPath;
#else
_basePath = Application.persistentDataPath;
#endif
        _path = _basePath + "/ConfigurationData";

        Data = new SaveData_Configuration();
        Data.ApplyData();
        LoadData();
    }
    #endregion
    public override void SaveData()
    {
        string Json = JsonUtility.ToJson(Data, true);
        //Debug.Log(Json);
        File.WriteAllText(_path, Json);
        PlayerPrefs.SetString("MyJson", Json);
    }
    public override void LoadData()
    {
        if (Directory.Exists(_basePath) && File.Exists(_path))
        {
            string json = PlayerPrefs.GetString("MyJson", File.ReadAllText(_path));
            JsonUtility.FromJsonOverwrite(json, Data);
        }
    }
}
