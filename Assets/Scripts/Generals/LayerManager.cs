using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class LayerManager : MonoBehaviour
{
    public static LayerManager Instance;
    Dictionary<EnumLayers, LayerMask> _layers = new Dictionary<EnumLayers, LayerMask>();
    [SerializeField] private LayerMask _interactMask;
    [SerializeField] private LayerMask _obstacleMask;
    [SerializeField] private LayerMask _obstaclesWithPlayerMask;

    private void Awake()
    {
        Instance = this;
        _layers.Add(EnumLayers.InteractMask, _interactMask);
        _layers.Add(EnumLayers.ObstacleMask, _obstacleMask);
        _layers.Add(EnumLayers.ObstacleWithPlayerMask, _obstaclesWithPlayerMask);
    }


    public LayerMask GetLayerMask(EnumLayers mask)
    {
        return _layers[mask];
    }
}
