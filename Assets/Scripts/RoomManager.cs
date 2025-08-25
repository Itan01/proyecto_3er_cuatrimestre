using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<AbstractEnemy> _enemies;
    [SerializeField] private List<AbstractObjects> _objects;
    private GameObject _smoke;
    private bool _doorBroken=false;
    [SerializeField] private bool _isActivate;

    public event Action DetPlayer, ResetDet, FindPlayer, DesActRoom, ActRoom;

    private void Awake()
    {

    }

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            GameManager.Instance.AddRoom(this);
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
            GameManager.Instance.RemoveRoom(this);
            _isActivate = false;
            DesactivateRoom();
        }
    }

    public void SetSmoke(GameObject smoke)
    {
        _smoke = smoke;
    }
    public void AddToList(AbstractEnemy Enemies)
    {
        _enemies.Add(Enemies);
    }
    public void AddToList(AbstractObjects Object)
    {
        _objects.Add(Object);
    }
    public void ResetRoom()
    {
        //foreach(var item in _objects)
        //{
        //    item.gameObject.SetActive(true);
        //}
        foreach (var item in _enemies)
        {
            item.Respawn();
        }
    }
    private void SummonSmoketrap()
    {
        if (_smoke == null) return;
        _smoke.SetActive(true);
        foreach (var item in _enemies) 
        { 
            item.gameObject.SetActive(false);
        }

    }
    public void SetDoorDestroyed()
    {
        _doorBroken = true;
    }
    public List<AbstractEnemy> GetEnemies()
    {
        return _enemies;
    }
    public void RemoveEnemy(AbstractEnemy enemy)
    {
        if (_enemies.Contains(enemy))
        {
            _enemies.Remove(enemy);
        }
    }

    public void DetectPlayer()
    {
        if (DetPlayer != null)
        DetPlayer();
    }
    public void ResetDetection()
    {
        if (ResetDet != null)
            ResetDet();
    }

    public void WatchPlayer()
    {
        if (FindPlayer != null)
            FindPlayer();
    }
    public void DesactivateRoom()
    {
        if (DesActRoom != null)
            DesActRoom();
    }
    public void ActivateRoom()
    {
        if (ActRoom != null)
            ActRoom();
    }
    public bool IsRoomActivate()
    {
        return _isActivate;
    }
}
