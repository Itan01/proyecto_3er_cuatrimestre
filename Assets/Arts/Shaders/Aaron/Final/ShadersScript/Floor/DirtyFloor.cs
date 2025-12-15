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
        // Clonamos el material para no modificar el original
        materialInstance = floorRenderer.material;
        materialInstance.SetFloat("_DirtAmount", dirtAmount);
    }

    void Update()
    {

        // Aumenta la suciedad con el tiempo
        dirtAmount += Time.deltaTime * dirtSpeed;
        dirtAmount = Mathf.Clamp01(dirtAmount);


        materialInstance.SetFloat("_DirtAmount", dirtAmount);
    }
}
