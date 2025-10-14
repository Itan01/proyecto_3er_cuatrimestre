using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private GameObject _smoke;
    private bool _doorBroken = false;
    [SerializeField] private bool _isActivate;
    public event Action DesActRoom, DestroyRoom, ActRoom;

    private void Start()
    {
        if(!_isActivate)
            DesactivateRoom();
    }

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            LVLManager.Instance.AddRoom(this);
            ActivateRoom();
            IsRoomActivate = true;
        }

    }

    private void OnTriggerExit(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            if (_doorBroken)
                SummonSmoketrap();
            LVLManager.Instance.RemoveRoom(this);
            IsRoomActivate = false;
            DesactivateRoom();
        }
    }

    public void SetSmoke(GameObject smoke)
    {
        _smoke = smoke;
    }

    private void SummonSmoketrap()
    {
        _smoke?.SetActive(true);
        DestroyRoom?.Invoke();
    }

    public void SetDoorDestroyed() => _doorBroken = true;
    public void DesactivateRoom() => DesActRoom?.Invoke();// Es lo mismo que if solo que en una linea
    public void ActivateRoom() => ActRoom?.Invoke();
    public bool IsRoomActivate
    {
        get { return _isActivate; }
        set { _isActivate = value; }
    }
}
