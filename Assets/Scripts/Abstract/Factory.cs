using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Factory<T> : MonoBehaviour
{
    [SerializeField] protected T _prefab;
    protected ObjectPool<T> _pool;
    [SerializeField] protected int _initAmount;

    protected virtual void Awake()
    {
        _pool = new ObjectPool<T>(CreatePrefab, Activate, Desactivate, _initAmount);
    }

    public abstract T Create();
    protected abstract T CreatePrefab();
    protected abstract void Desactivate(T obj);
    protected abstract void Activate(T obj);

}
