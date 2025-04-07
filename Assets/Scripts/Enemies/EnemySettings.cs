using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySettings : MonoBehaviour
{
    [SerializeField] private float _movSpeed = 5.0f;
    //[SerializeField] private float _maxResistance =2.0f;
    private EnemyMovementTypeOne _setOne;
    private EnemyMovementTypeTwo _setTwo;
    private SoundMov _script;
    private Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        //_rb.freezeRotation = true;
        _setOne = GetComponent<EnemyMovementTypeOne>();
        _setTwo = GetComponent<EnemyMovementTypeTwo>();
        SetTypeOfMovement(1);
    }

    public void SetTypeOfMovement( int IndexMovement)
    {


        if (IndexMovement == 1) 
        {
            _setOne.SetActivate(true, _movSpeed);
            _setTwo.SetActivate(false, 3.5f);
        }
        if (IndexMovement == 2)
        {
            _setOne.SetActivate(false, 3.5f);
            _setTwo.SetActivate(true, Mathf.Clamp(_movSpeed, 0.0f,5.0f));
        }
        else
        {
            _setOne.SetActivate(true, _movSpeed);
        }
    }

    void OnTriggerEnter(Collider sound)
    {
        if (sound.gameObject.CompareTag("sound"))
        {
            _script = sound.GetComponent<SoundMov>();
            SetTypeOfMovement(2);
            _setTwo.SetPostionToFollow(new Vector3(_script._startPoint.x,transform.position.y,_script._startPoint.z));
            Destroy(sound.gameObject);
        }
    }
}
