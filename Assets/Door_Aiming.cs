using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_Aiming : MonoBehaviour, ISoundAim
{
    [SerializeField] private MeshRenderer _mesh;
    private void Start()
    {
        if (_mesh == null) _mesh = GetComponent<MeshRenderer>();
    }
    public void Activate()
    {
        Material Material = _mesh.material;
        Material.SetFloat("_Aiming", 1.0f);
    }

    public void Deactivate()
    {
        Material Material = _mesh.material;
        Material.SetFloat("_Aiming", 0.0f);
    }
}
