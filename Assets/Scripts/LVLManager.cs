using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LVLManager : MonoBehaviour
{
    /*Variable Globales que se necesita en el Nivel*/
    #region Sigleton
    public static LVLManager Instance;

    private void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
    #endregion
    private Vector3 _checkPoint;
    public Vector3 Respawn
    {
        get { return _checkPoint; }
        set { _checkPoint = value; }

    }
    private List<RoomManager> _rooms= new List<RoomManager>();
    public void AddRoom(RoomManager Room)
    {
        if (!_rooms.Contains(Room))
        {
            _rooms.Add(Room);
        }
    }
    public void RemoveRoom(RoomManager Room)
    {
        if (_rooms.Contains(Room))
        {
            _rooms.Remove(Room);
        }
    }
    private Abstract_Weapon _gun;
    public Abstract_Weapon Gun
    {
        get { return _gun; }
        set { _gun = value; }
    }
}
