using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleItem : MonoBehaviour, IInteractableObject
{
    [SerializeField] private float _duration;
    [SerializeField] Color _color;
    private void Start()
    {

    }

    public void OnInteract()
    {
        Debug.Log("HI");
        GameManager.Instance.PlayerReference.SetInvisiblePowerUp(_duration);
        AudioClip _clip = AudioStorage.Instance.GunSound(EnumAudios.GunShooting);
        AudioManager.Instance.PlaySFX(_clip,1.0f);
        Material _material = GameManager.Instance.PlayerReference.GetComponentInChildren<SkinnedMeshRenderer>().material;
        _material.SetColor("_Color", _color);
        Destroy(gameObject);
    }
}
