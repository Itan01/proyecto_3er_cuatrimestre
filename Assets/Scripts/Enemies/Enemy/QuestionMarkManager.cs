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
    }

    public void Setting(bool State, int index) // 0 = questionMark, 1 = ExclamationMark
    {
        gameObject.SetActive(State);
        _render.sprite = _spritesStates[index];
    }
}
