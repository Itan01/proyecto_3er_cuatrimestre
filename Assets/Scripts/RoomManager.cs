using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private List<AbstractEnemy> _enemies;
    [SerializeField] private List<Light> _light;
    [SerializeField] private List<AbstractObjects> _objects;
    [SerializeField] private List<BaseDoor> _doors;
    [SerializeField] private List<CameraObstacleController> _camera;
    [SerializeField] private List<RoombaEnemy> _roomba;
    private void Awake()
    {

    }

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            GameManager.Instance.AddRoom(this);
            foreach (var item in _light)
            {
                item.gameObject.SetActive(true);
            }
            foreach (var item in _enemies)
            {
                item.SetActivate(true);
            }
        }

    }

    private void OnTriggerExit(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            GameManager.Instance.RemoveRoom(this);
            foreach (var item in _objects)
            {
                if (item.gameObject.activeSelf)
                {
                    _objects.Remove(item);
                }
            }
            foreach (var item in _light)
            {
                item.gameObject.SetActive(false);
            }
            foreach (var item in _enemies)
            {
                item.SetActivate(false);
            }
        }
    }
    //<summary>
    //    Agrega a la lista para tener el control a la hora de activarlo/Desactivarlo
    //<summary>
    public void AddToList(AbstractEnemy Enemies)
    {
        _enemies.Add(Enemies);
    }
    public void AddToList(Light Lights)
    {
        _light.Add(Lights);
    }
    public void AddToList(AbstractObjects Object)
    {
        _objects.Add(Object);
    }
    public void AddToList(BaseDoor Door)
    {
        _doors.Add(Door);
    }
    public void AddToList(CameraObstacleController Camera)
    {
        _camera.Add(Camera);
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

    public void OpenOrCloseBaseDoors(bool State)
    {
        foreach (var Door in _doors)
        {
            Door.ForceDoorsClose(State);
        }
    }
    public void CallRobots()
    {
        foreach (var Roomba in _roomba)
        {
            Roomba.SetActivate(true);
        }
    }
    public void CameraDetection()
    {
        CallRobots();
        OpenOrCloseBaseDoors(true);
    }
    public void CameraResetDetection()
    {
        OpenOrCloseBaseDoors(false);
    }
}
