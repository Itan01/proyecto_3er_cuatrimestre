using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UISetSound : MonoBehaviour
{
    private Image _uI;
    private Color _color;
    private Sprite _noSound;
    private Animator _animator;
    private void Start()
    {
        UIManager.Instance.UISound = this;
        _animator= GetComponent<Animator>();
        _uI = GetComponent<Image>();
        _noSound=_uI.sprite;
        _uI.color = _color;
    }

    private void Update()
    {
        
    }
    public void SetSound(int index)
    {
        SoundStruct SoundRef= GameManager.Instance.SoundsReferences.GetSoundComponents(index);
        _animator.SetBool("Grabbing",true);
        _uI.sprite = SoundRef.SpriteUi;
    }
    public void Shooting()
    {
        _animator.SetTrigger("Shooting");
        _animator.SetBool("Grabbing", false);
    }

    public void SetNoSound()
    {
        _uI.sprite= _noSound;
    }
    public void ClearSound()
    {
        _uI.sprite = _noSound;
        _animator.SetBool("Grabbing", false); 
    }
}
