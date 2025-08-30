using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIMenuCadenzaTransition : MonoBehaviour
{
    private Animation _animation;
    private void Start()
    {
        _animation=GetComponent<Animation>();
        Time.timeScale = 1f;
    }
    public void PlayAnimation()
    {
        _animation.Play();
    }
    private void AnimationIsFInished()
    {
        MainManager.Instance.SetAnimationIsPlaying = false;
    }
    private void AnimationStillPlaying()
    {
        MainManager.Instance.SetAnimationIsPlaying = true;
    }
}
