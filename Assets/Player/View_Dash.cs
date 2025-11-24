
using UnityEngine;

public class View_Dash : Abstract_View
{
    private AudioClip _clip;
    public View_Dash()
    {
        _clip = AudioStorage.Instance.PlayerSound(EAudios.PlayerDash);
    }
    public void Execute()
    {
        AudioManager.Instance.PlaySFX(_clip,1.0f);
        EventPlayer.Trigger(EPlayer.dash,3.0f);
        _animator.SetTrigger("Dash");
    }
}
