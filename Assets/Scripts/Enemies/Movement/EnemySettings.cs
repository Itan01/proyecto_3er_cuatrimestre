using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySettings : MonoBehaviour
{
/*    private EnemyMovementTypeOne _setOne;
    private EnemyMovementTypeTwo _setTwo;
    private int _index = 1;
    //private SoundMovement _script;
    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.freezeRotation = true;
        _setOne = GetComponent<EnemyMovementTypeOne>();
        _setTwo = GetComponent<EnemyMovementTypeTwo>();
        SetTypeOfMovement(_index);
    }

    public void SetTypeOfMovement( int IndexMovement)
    {
        _index = IndexMovement;

        if (_index == 1) 
        {
            _setOne.SetActivate(true);
            _setTwo.SetActivate(false);
        }
        else if (_index == 2)
        {
            _setOne.SetActivate(false);
            _setTwo.SetActivate(true);
        }
        else
        {
            _setOne.SetActivate(true);
            _setTwo.SetActivate(false);
        }
    }

    void OnTriggerEnter(Collider sound)
    {
        if (sound.gameObject.CompareTag("sound"))
        {
        //    _script = sound.GetComponent<SoundMovement>();
            SetTypeOfMovement(2);
            //_setTwo.SetPostionToFollow(new Vector3(_script._startPoint.x,transform.position.y,_script._startPoint.z));
            Destroy(sound.gameObject);
        }
    }
*/
}
