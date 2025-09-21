using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cons_CamColorLight
{
    private Renderer _camRenderer;
    private Light _light;
    private Color _color;
    private float _intensity;
    public Cons_CamColorLight(Renderer Render, Light Light, Color Color, float intensity =999f)
    {
        _camRenderer = Render;
        _light = Light;
        _color = Color;
        _intensity = intensity;
    }

    public void SetCameraColor()
    {
        _camRenderer.material.SetColor("_Color", _color);
        _camRenderer.material.SetColor("_EmissionColor", _color);
        _light.color = _color;
        _light.intensity = _intensity;
    }
}
