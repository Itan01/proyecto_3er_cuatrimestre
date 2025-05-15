using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropWater : MonoBehaviour
{
    private Vector3 _originPost;
    public string groundtag = "Floor";
    public float waitTime = 5f;
    //public GameObject soundLiquid;
    private Rigidbody _rb;
    private bool _isReset = false;

    private void Start()
    {
        _originPost = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!_isReset && collision.gameObject.CompareTag(groundtag))
        {
            _isReset = true;
            StartCoroutine(ResetAfterWait());
        }
    }
    private IEnumerator ResetAfterWait()
    {
        if (_rb != null)
        {
            _rb.isKinematic = true;
            _rb.velocity = Vector3.zero;
            _rb.angularVelocity = Vector3.zero;
        }

        yield return new WaitForSeconds(waitTime);

        transform.position = _originPost;
        
        if (_rb != null)
        {
            _rb.isKinematic = false;
        }

        _isReset = false;
    }

}
