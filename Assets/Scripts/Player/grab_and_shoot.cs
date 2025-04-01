using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class grab_and_shoot : MonoBehaviour
{
    private bool _hasASound;
    private GameObject _soundShooot;
    [SerializeField] private GameObject _AreaToCatch;
    private string _useMegaphone="x";
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!_hasASound && Input.GetKeyDown(_useMegaphone))
        {
            CatchASound();
        }
    }

    public void ChangeTypeOfSound()
    {

    }
    private void CatchASound()
    {
        var CatchingSound = Instantiate(_AreaToCatch,transform.position,Quaternion.identity);
    }
}
