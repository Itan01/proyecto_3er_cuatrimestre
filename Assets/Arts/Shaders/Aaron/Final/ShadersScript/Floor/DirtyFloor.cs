using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtyFloor : MonoBehaviour
{
    [SerializeField] Renderer floorRenderer;
    [SerializeField] float dirtSpeed = 3f;

    float dirtAmount = 0f;
    Material materialInstance;

    void Start()
    {
        materialInstance = floorRenderer.material;
        materialInstance.SetFloat("_DirtAmount", dirtAmount);
    }

    void Update()
    {
        dirtAmount += Time.deltaTime * dirtSpeed;
        dirtAmount = Mathf.Clamp(dirtAmount, 0f, 0.65f);

        materialInstance.SetFloat("_DirtAmount", dirtAmount);
    }
}
