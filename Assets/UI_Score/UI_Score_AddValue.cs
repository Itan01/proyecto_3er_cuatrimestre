using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UI_Score_AddValue : MonoBehaviour, IObserverScore
{
    private TextMeshProUGUI _text;
    private Animation _animation;
    void Start()
    {
        GetComponentInParent<IObservableScore>().AddObs(this);
        _text = GetComponent<TextMeshProUGUI>();
        _animation= GetComponent<Animation>();  
    }
    public void Execute(float Value, float AddValue)
    {
        int Aux = (int)AddValue;
        _text.text= $"${Aux.ToString()}";
        _animation.Play();
    }
}
