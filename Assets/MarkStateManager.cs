using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class MarkStateManager : MonoBehaviour
{
    private SpriteRenderer _render;
    private Cons_LockOnTarget _billboard;

    private void Start()
    {
        _billboard = new Cons_LockOnTarget(transform);
        _render = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        _billboard.Lock();
    }
    public void SetMark(bool ShowMark, Sprite Sprite)
    {
        gameObject.SetActive(ShowMark);
        _render.sprite = Sprite;

    }
}