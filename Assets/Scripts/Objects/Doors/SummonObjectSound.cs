using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SummonObjectSound : MonoBehaviour
{
    private Vector3 _orientation, _selfPosition;
    private Animation _animator;
    [SerializeField] private GameObject _soundToSummon;
    private AbstractSound _settings;
    void Start()
    {
        _animator = GetComponent<Animation>();
    }

    private void OnTriggerEnter(Collider Player)
    {
        if (Player.gameObject.CompareTag("Player"))
        {
            if(_animator.isPlaying) return;
            _animator.Play();
            _selfPosition = transform.position + new Vector3(0.0f, 1.0f, 0.0f);
            var _sound = Instantiate(_soundToSummon, _selfPosition, Quaternion.identity);
            _orientation = (transform.position - Player.transform.position).normalized*2 + _selfPosition;
            _settings = _sound.GetComponent<AbstractSound>();
            _settings.SetDirection(_orientation, Random.Range(3.0f,7.0f +1), 1.0f);
        }
    }
}
