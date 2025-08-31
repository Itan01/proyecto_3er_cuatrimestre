using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionMarkManager : MonoBehaviour
{
    [SerializeField] private Sprite[] _spritesStates= new Sprite[2];
    private SpriteRenderer _render;

    private void Start()
    {
        _render = GetComponent<SpriteRenderer>();
    }

    public void SetMark(bool State, int index) //true=ShowMark, false=HideMark / 0 = questionMark, 1 = ExclamationMark
    {
        _render.sprite = _spritesStates[index];
        gameObject.SetActive(State);
    }
}
