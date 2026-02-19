using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public abstract class Abstract_SaveData : MonoBehaviour
{
    protected string _basePath;
    protected string _path;
    public abstract void SaveData();
    public abstract void LoadData();
    public void Erase()
    {
        if (Directory.Exists(_basePath) && File.Exists(_path))
        {
            File.Delete(_path);
        }
    }
    protected virtual void OnApplicationPause(bool pause)
    {
        if (pause) SaveData();
    }
}
