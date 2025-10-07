using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CollisionToHear : MonoBehaviour
{
    private AbstractEnemy _scriptManager;
    private PlayerManager _player;
    [SerializeField] private float _distance=7.5f;
    private void Start()
    {
        _player = GameManager.Instance.PlayerReference;
        _scriptManager = GetComponentInParent<AbstractEnemy>();
    }

    private void Update()
    {
        if (_player.IsDeath() || !_player.IsMoving() || _player.IsCrouching()) return;
        if ((_player.transform.position - transform.position).magnitude <= _distance)
        {
            if (_scriptManager.GetMode() <= 0 && _scriptManager.GetMode() != 1)
            {
                _scriptManager.SetPosition(_player.transform.position);
                _scriptManager.SetModeByIndex(2);
            }
        }
    }
}
