using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyMovPattern _scriptPattern;
    private EnemyMovFollowTarget _scriptFollow;
    private AbsStandardSoundMov _scriptSound;
    private PlayerSetCheckpoint _scriptPlayer;
    private int _index = 1;

    void Start()
    {
        _scriptPattern = GetComponent<EnemyMovPattern>();
        _scriptFollow = GetComponent<EnemyMovFollowTarget>();
        SetTypeOfMovement(_index);
    }

    public void SetTypeOfMovement(int IndexMovement)
    {
        _index = IndexMovement;

        if (_index == 1)
        {
            _scriptPattern.SetActivate(true);
            _scriptFollow.SetActivate(false);
        }
        else if (_index == 2)
        {
            _scriptPattern.SetActivate(false);
            _scriptFollow.SetActivate(true);
        }
        else
        {
            _scriptPattern.SetActivate(true);
            _scriptFollow.SetActivate(false);
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
        if (Obj.gameObject.CompareTag("Player"))
        {
            _scriptPlayer = Obj.gameObject.GetComponent<PlayerSetCheckpoint>();
            _scriptPlayer.MoveToCheckPoint();
            _scriptFollow.Reset();
            SetTypeOfMovement(1);
        }
    }
}
