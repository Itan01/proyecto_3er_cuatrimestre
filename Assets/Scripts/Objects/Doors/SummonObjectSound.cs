using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonObjectSound : MonoBehaviour
{
    private Vector3 _orientation, _selfPosition;

    [SerializeField] private GameObject _soundToSummon;
    private AbsStandardSoundMov _settings;
    void Start()
    {
        
    }
    private void OnCollisionEnter(Collision player)
    {
        if (player.gameObject.CompareTag("Player"))
        {
            _selfPosition = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
            var _sound = Instantiate(_soundToSummon, _selfPosition, Quaternion.identity);
            _orientation = (transform.position - player.transform.position).normalized*2 + _selfPosition;
            _settings = _sound.GetComponent<AbsStandardSoundMov>();
            _settings.Spawn(_selfPosition, _orientation, Random.Range(3.0f,7.0f +1), 1.0f);
            Destroy(gameObject);
        }
    }
}
