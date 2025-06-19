using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEditorInternal;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField]private List<AbstractEnemy> _enemies;
    [SerializeField] private List<Light> _light;
    [SerializeField] private List<AbstractObjects> _objects;

    private void Awake()
    {

    }

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.GetComponent<PlayerManager>())
        {
            GameManager.Instance.Room = this;
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

    public void ResetRoom()
    {
        //foreach(var item in _objects)
        //{
        //    item.gameObject.SetActive(true);
        //}
        foreach(var item in _enemies)
        {
            item.Respawn();
        }
    }

}
