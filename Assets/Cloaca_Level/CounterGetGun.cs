using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterGetGun : MonoBehaviour
{
    private int _index=0;
   [SerializeField] private GameObject _gun;
    [SerializeField] private PlayerManager _player;
    private void Start()
    {
        StartCoroutine(Setter());
    }
    public void Add()
    {
        _index++;
        if (_index >=3)
        {
            _gun.SetActive(true);
//GameManager.Instance.PlayerReference.CanUseMegaphone(true);
        }
    }
    private IEnumerator Setter()
    {
        yield return new WaitForSeconds(0.1f);
      //  _player.CanUseMegaphone(false);
    }
}
