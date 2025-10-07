using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T>
{
    public delegate T Method();
     Method _factoryCreate;
     Action<T> _turnOn;
     Action<T> _turnOff;
     List<T> _stock = new List<T>();

    public ObjectPool(Method FactoryMethod, Action<T> TurnOn, Action<T> TurnOFF, int initialState=5)
    {
        _factoryCreate = FactoryMethod;
        _turnOn = TurnOn;
        _turnOff = TurnOFF;
        for (int i = 0; i < initialState; i++)
        {
            var x = _factoryCreate();
            _turnOff(x);
            _stock.Add(x);
        }
    }

    public T Get() 
    {
        if (_stock.Count > 0)
        {
            var x = _stock[0];
            _stock.RemoveAt(0);
            _turnOn(x);
            return x;
        }
        return _factoryCreate();
    }
    public void Release(T obj)
    {
        _turnOff(obj);
        _stock.Add(obj);
    }
}

