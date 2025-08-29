using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_enemy_vision : MonoBehaviour
{
  [Range(1, 360)][SerializeField] private float _viewAngle = 90f;
   // [Range(1, 180)][SerializeField] private float _verticalFOV = 60f;
   // [SerializeField] private float _radius = 10f;
    //[SerializeField] private int _HorizontalCounts = 20;
    //[SerializeField] private int _verticalCounts = 20;

    [Header("Layer Masks")]
    private LayerMask _obstacleMask;
    private LayerMask _detectableMask;

    [Header("References")]
    private AbstractEnemy _scriptManager;
    private PlayerManager _player;

    [Header("Debug")]
    private bool _seePlayer = false;

    private Cons_Raycast _raycast;

    private Transform _headReference;
    private void Start()
    {
        _scriptManager = GetComponentInParent<AbstractEnemy>();
        _headReference = _scriptManager.GetHeadTransform();
        _player = GameManager.Instance.PlayerReference;
        _detectableMask = LayerManager.Instance.GetLayerMask(EnumLayers.ObstacleWithPlayerMask);
        _obstacleMask = LayerManager.Instance.GetLayerMask(EnumLayers.ObstacleMask);
        _raycast = new Cons_Raycast(500f, _detectableMask);
    }

    private void LateUpdate()
    {
        if (_player.IsPlayerDeath() || _player.GetInvisible()) return;
        CanSeePlayer();
    }

    private void CanSeePlayer()
    {
        Vector3 dir = (_player.transform.position - transform.position).normalized;
        if (Vector3.Angle(transform.position, dir) < _viewAngle)
            CheckIfHasVIsion();
        else
            Debug.Log("No Estoy a Nadie");
    }

    private void CheckIfHasVIsion()
    {
        _seePlayer = false;
        Vector3 PlayerHead = (_player.GetHeadPosition() - _headReference.position).normalized;
        Vector3 PlayerHips = (_player.GetHipsPosition() - _headReference.position).normalized;
        Vector3 PlayerPosition = (_player.transform.position - _headReference.position).normalized;
        if (_raycast.Checker<PlayerManager>(_headReference.position, PlayerHead) ||
            _raycast.Checker<PlayerManager>(_headReference.position, PlayerHips) ||
            _raycast.Checker<PlayerManager>(_headReference.position, PlayerPosition))
        {
            _seePlayer = true;
            Debug.Log("Estoy Viendo Al Jugador");
            if (_scriptManager.GetMode() != 3 && _scriptManager.GetMode() != 1 && _scriptManager.GetMode() != 6)
                _scriptManager.SetModeByIndex(3);
        }
        _scriptManager.WatchingPlayer(_seePlayer);
    }
}
