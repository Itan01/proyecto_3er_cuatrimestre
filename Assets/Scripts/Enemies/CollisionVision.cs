using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionVision : MonoBehaviour
{
    [SerializeField]private EnemyMovFollowTarget _scriptFollow;
    [SerializeField] private EnemyController _scriptControl;
    private void Start()
    {
        _scriptControl = GetComponentInParent<EnemyController>();
        _scriptFollow = GetComponentInParent<EnemyMovFollowTarget>();
    }
    void OnTriggerEnter(Collider player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            if (!_scriptFollow._hasTarget)
            {
                _scriptFollow.SetTargetToFollow(player.gameObject.transform);
                _scriptControl.SetTypeOfMovement(2);
            }

        }
    }
}
