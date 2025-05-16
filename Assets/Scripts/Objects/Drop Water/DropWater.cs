using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class DropWater : MonoBehaviour
{
    private Vector3 _originPost;
    public float waitTime = 5f;
    public GameObject soundLiquid;
    private Rigidbody _rb;
    private bool _isReset = false;

    private void Start()
    {
        _originPost = transform.position;
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!_isReset)
        {
            SummonSound();
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

    private void SummonSound()
    {
        var Sound = Instantiate(soundLiquid,transform.position, Quaternion.identity);
        Vector3 RandomDirection = new Vector3(Random.Range(-1.0f,1.0f), 0.1f, Random.Range(-1.0f,1.0f));
        Sound.GetComponent<AbstractSound>().SetDirection(RandomDirection, 3.5f, 1.0f);
    }

}
