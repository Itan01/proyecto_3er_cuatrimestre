using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbstracDoors : MonoBehaviour, ISoundInteractions
{
    [SerializeField] protected int _maxValue;
    protected int _indexToDestroy;
    [SerializeField] protected MeshRenderer _mesh;
    [SerializeField] protected SetCountDoor[] _scriptText;
    protected bool _isDestroyed;
    protected virtual void Start()
    {
        
        if (_maxValue == 0)
        {
            _maxValue = 1;
        }
        _indexToDestroy = _maxValue;
        for (int i = 0; i < _scriptText.Length; i++)
        {
            _scriptText[i].SetValue(_indexToDestroy, _maxValue);
        }
        
    }
    protected virtual void Update()
    { }
    public virtual void CheckStatus()
    {
        if (_isDestroyed) return;
        _indexToDestroy--;
        for (int i = 0; i < _scriptText.Length; i++)
        {
            _scriptText[i].SetValue(_indexToDestroy, _maxValue);
        }
        if (_indexToDestroy <= 0)
        {
            OpenDoor();
        }
        else
            StartCoroutine(RecievedSound());

    }
    protected virtual void OpenDoor()
    {
        for (int i = 0; i < _scriptText.Length; i++)
        {
            _scriptText[i].gameObject.SetActive(false);
        }
        StartCoroutine(DestroyDoor());
        _isDestroyed = true;

    }
    protected IEnumerator RecievedSound()
    {
        float Intensity = 0.0f;
        bool Backing = false;
        Material Material = _mesh.material;
        Material.SetFloat("_Recieved_Sound", 1.0f);
        while (Intensity < 1.0f && !Backing)
        {
            Intensity += Time.deltaTime * 4;
            Material.SetFloat("_Intensity",Intensity);
            yield return null;
        }
        if (Intensity >= 1.0f)
            Backing = true;
        while (Intensity > 0.0f && Backing)
        {
            Intensity -= Time.deltaTime;
            Material.SetFloat("_Intensity", Intensity);
            yield return null;
        }
        Material.SetFloat("_Recieved_Sound", 0.0f);
        yield return null;

    }
    protected IEnumerator DestroyDoor()
    {
        float Intensity = 1.0f;
        Material Material = _mesh.material;
        Material.SetFloat("_Destroyed", 1.0f);
        while (Intensity > 0.0f)
        {
            Intensity -= Time.deltaTime;
            Material.SetFloat("_Intensity", Intensity);
            yield return null;
        }
        Material.SetFloat("_Destroyed", 0.0f);
        yield return null;

    }
}
