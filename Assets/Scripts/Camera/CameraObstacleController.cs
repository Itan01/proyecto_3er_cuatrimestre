using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraObstacleController : MonoBehaviour
{
    [SerializeField]private Transform _target;
    private Animation _animation;

    private void Start()
    {
        _animation = GetComponent<Animation>();
        _animation.Play();
    }

    private void Update()
    {
        if(_target)
        {
            _animation.Stop();
            transform.LookAt(_target.position);
        }
        else
        {
            if (_animation.isPlaying) return;
            _animation.Play();
        }
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }
}
