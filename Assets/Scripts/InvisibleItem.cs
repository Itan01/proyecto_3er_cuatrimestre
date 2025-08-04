using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleItem : MonoBehaviour, IInteractableObject
{
    [SerializeField] private float _duration;
    [SerializeField] Color _color;
    private Material _material;

    public void OnInteract()
    {
        Debug.Log("HI");
        _material=GameManager.Instance.PlayerReference.GetComponentInChildren<SkinnedMeshRenderer>().material;
        AudioStorage.Instance.ShootingSound();
        GameManager.Instance.PlayerReference.SetInvisiblePowerUp(_duration);
        _material.SetColor("_Color", _color);
        Destroy(gameObject);
    }
}
