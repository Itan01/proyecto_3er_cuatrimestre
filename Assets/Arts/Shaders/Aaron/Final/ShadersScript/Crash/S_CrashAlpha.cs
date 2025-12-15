using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_CrashAlpha : MonoBehaviour
{
    public float fadeDuration = 4f;

    private Material material;
    private Color baseColor;

    void Start()
    {
        material = GetComponent<Renderer>().material;

        baseColor = material.GetColor("_BaseColor");

        StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);

            baseColor.a = alpha;
            material.SetColor("_BaseColor", baseColor);

            yield return null;
        }

        baseColor.a = 0f;
        material.SetColor("_BaseColor", baseColor);
    }
}
