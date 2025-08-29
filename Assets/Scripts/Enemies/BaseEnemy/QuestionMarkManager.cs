using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMarkManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _spritesStates;
    private SpriteRenderer _render;

    private void Start()
    {
        _render = GetComponent<SpriteRenderer>();
        SetMark(false, 0);
    }

    public void SetMark(bool State, int index) //true=ShowMark, false=HideMark / 0 = questionMark, 1 = ExclamationMark
    {
        if(State)
        _render.sprite = _spritesStates[index];
        else
            _render.sprite = null;
    }
}
