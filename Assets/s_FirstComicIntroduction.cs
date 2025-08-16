using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class s_FirstComicIntroduction : MonoBehaviour
{
    [SerializeField]private Image _image;
    private void Start()
    {
        if (!GameManager.Instance.ShowComicEntry)
        {
            gameObject.SetActive(false);
        }
        else
            GameManager.Instance.PlayerReference.SetIfPlayerCanMove(false);
        _image.GetComponent<Image>();
    }
    public void SetSprite( Sprite sprite)
    {
        _image.sprite= sprite;
    }
    public void FirstSprite(Sprite sprite)
    {
        _image.color=Color.white;
        _image.sprite = sprite;
    }


    public void Desactivate() 
    {
        StartCoroutine(FadeOut());

    }

    private IEnumerator FadeOut()
    {
        yield return new WaitForSeconds(0.25f);
        Color opacty = _image.color;
        while (opacty.a >0)
        {
            opacty.a -= Time.deltaTime;
            _image.color = opacty;
            if (Input.anyKeyDown)
                opacty.a = 0.0f;
            yield return null;
        }
        GameManager.Instance.PlayerReference.SetIfPlayerCanMove(true);
        yield return new WaitForSeconds(0.25f);
        gameObject.SetActive(false);

    }
}
