using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PlayerScore
{
    private int _score= 0;
    private TMP_Text _pointsUI;
    private string _mainText="Score: ", _textUI;

    public PlayerScore(TMP_Text TextUi)
    {
        _pointsUI=TextUi;
        _pointsUI.text = ($"{_mainText}000");
    }
    public void SetScore (int addPoints)
    {
        _score += addPoints;
        _pointsUI.text = ($"{_mainText}{_score}");
    }
}
