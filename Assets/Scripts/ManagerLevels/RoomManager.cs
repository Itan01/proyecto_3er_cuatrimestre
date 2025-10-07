using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    private GameObject _smoke;
    private bool _doorBroken = false;
    [SerializeField] private bool _isActivate;
    public event Action DetPlayer, ResetDet, FindPlayer, DesActRoom, DestroyRoom, ActRoom, ResRoom, ResPath;

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
            _isActivate = true;
        }

    }

    private void OnTriggerExit(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            if (_doorBroken)
                SummonSmoketrap();
            LVLManager.Instance.RemoveRoom(this);
            _isActivate = false;
            DesactivateRoom();
        }
    }

    public void SetSmoke(GameObject smoke)
    {
        _smoke = smoke;
    }
    public void ResetRoom() => ResRoom?.Invoke(); // Es lo mismo que if solo que en una linea

    private void SummonSmoketrap()
    {
        _smoke?.SetActive(true);
        DestroyRoom?.Invoke();
    }

    public void SetDoorDestroyed() => _doorBroken = true;
    public void DetectPlayer() => DetPlayer?.Invoke();
    public void ResetDetection() => ResetDet?.Invoke();
    public void WatchPlayer() => FindPlayer?.Invoke();
    public void DesactivateRoom() => DesActRoom?.Invoke();
    public void ActivateRoom() => ActRoom?.Invoke();
    public void ResetPath() => ResPath?.Invoke();
    public bool IsRoomActivate()
    {
        return _isActivate;
    }
}
