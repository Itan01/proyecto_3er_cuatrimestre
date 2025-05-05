using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerScore : MonoBehaviour
{
    private int _score= 0;
    [SerializeField] private TMP_Text _pointsUI;
    private string _mainText="Score: ", _textUI;

    private void Start()
    {
        _pointsUI.text = _pointsUI.text = ($"{_mainText}000"); ;
    }
    public void SetScore (int addPoints)
    {
        _score += addPoints;
        _pointsUI.text = ($"{_mainText}{_score}");
    }
}
