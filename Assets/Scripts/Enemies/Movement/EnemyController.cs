using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyMovPattern _scriptPattern;
    private EnemyMovFollowTarget _scriptFollow;
    private EnemyConfused _scriptConfused;
    private AbsStandardSoundMov _scriptSound;
    private PlayerSetCheckpoint _scriptPlayer;
    private int _index = 1;

    void Start()
    {
        _scriptPattern = GetComponent<EnemyMovPattern>();
        _scriptFollow = GetComponent<EnemyMovFollowTarget>();
        _scriptConfused = GetComponent<EnemyConfused>();
        SetTypeOfMovement(_index);
    }

    public void SetTypeOfMovement(int IndexMovement)
    {
        _index = IndexMovement;

        if (_index == 1) // Patrullar
        {
            _scriptPattern.SetActivate(true);
            _scriptFollow.SetActivate(false);
            _scriptConfused?.SetActivate(false);
        }
        else if (_index == 2) // Seguir sonido
        {
            _scriptPattern.SetActivate(false);
            _scriptFollow.SetActivate(true);
            _scriptConfused?.SetActivate(false);
        }
        else if (_index == 3) // Confundido
        {
            _scriptPattern.SetActivate(false);
            _scriptFollow.SetActivate(false);
            _scriptConfused?.SetActivate(true);
        }
    }

    void OnCollisionEnter(Collision Obj)
    {
        if (Obj.gameObject.CompareTag("Sound"))
        {
            _scriptSound = Obj.gameObject.GetComponent<AbsStandardSoundMov>();
            _scriptFollow.SetPostionToFollow(_scriptSound._startPosition);
            SetTypeOfMovement(2);
            Destroy(Obj.gameObject);
        }
        else if (Obj.gameObject.CompareTag("Explosion"))
        {
            SetTypeOfMovement(3);
            Destroy(Obj.gameObject);
        }
        else if (Obj.gameObject.CompareTag("Player"))
        {
            _scriptPlayer = Obj.gameObject.GetComponent<PlayerSetCheckpoint>();
            _scriptPlayer.MoveToCheckPoint();
            _scriptFollow.Reset();
            SetTypeOfMovement(1);
        }
    }
}

