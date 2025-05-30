using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionVision : MonoBehaviour
{
    private AbstractEnemy _scriptManager;
    private Ray _ray;
    [SerializeField]private LayerMask _layerMask;
    private RaycastHit _inHit;
    private Vector3 _PlayerReference;
    private bool _checkIfCanSeeit, _playerDeath=false;
    private void Start()
    {
        _scriptManager = GetComponentInParent<AbstractEnemy>();
        _PlayerReference = GameManager.Instance.PlayerReference.transform.position;
    }
    private void Update()
    {
        _playerDeath = GameManager.Instance.PlayerReference.IsPlayerDeath();
        if (_checkIfCanSeeit && _scriptManager.GetMode()!= 1 && !_playerDeath)
        {
            _PlayerReference = GameManager.Instance.PlayerReference.transform.position;
            CheckRaycast();
        }
        
    }
    void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.layer == 27)
        {
            _checkIfCanSeeit= true;
            
        }
    }
    void OnTriggerExit(Collider Player)
    {
        if (Player.gameObject.layer == 27)
        {
            _checkIfCanSeeit = false;
        }
    }
    private void CheckRaycast()
    {
        _ray = new Ray(transform.position+ new Vector3(0,1,0), (_PlayerReference-transform.position).normalized);
        if (Physics.Raycast(_ray,out _inHit,20.0f, _layerMask))
        {
            if (_inHit.collider.GetComponent<PlayerManager>())
            {
                _scriptManager.SetMode(1);
            }
        }
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position + new Vector3(0, 1, 0), (_PlayerReference-transform.position).normalized *20.0f);
    }
}
