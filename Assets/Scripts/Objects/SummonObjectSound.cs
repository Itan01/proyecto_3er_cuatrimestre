using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonObjectSound : MonoBehaviour
{
    private Vector3 _postionToSpawnSound;
    [SerializeField] private GameObject _soundToSummon;
    private SoundMov _settings;
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision player)
    {
        if (player.gameObject.CompareTag("Player"))
        {

            var _sound = Instantiate(_soundToSummon, transform.position+ new Vector3(0,1,0), Quaternion.identity);
            _postionToSpawnSound = (transform.position - player.transform.position).normalized + transform.position + new Vector3(0, 1, 0);
            _settings = _sound.GetComponent<SoundMov>();
            _settings.SetVector(_postionToSpawnSound,1.5f,"sonido1");
            Debug.Log(_postionToSpawnSound);
            Destroy(gameObject);
        }
    }
}
