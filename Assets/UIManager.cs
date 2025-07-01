using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    #region Sigleton
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;

    }

    #endregion
    private PlayerManager _player;

    private void Start()
    {
        _player = GameManager.Instance.PlayerReference;
        StartCoroutine(PlayAnimationAfterLoad());
    }

    public GameObject pauseMenu;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isMenuON)
                Resume();
            else
                Pause();
        }
    }

    public bool IsMenuActive()
    {
        return isMenuON;
    }

    private TransitionFade _scriptTransition;
    public TransitionFade Transition
    {
        get { return _scriptTransition; }
        set { _scriptTransition = value; }
    }

    private UISetSound _soundUI;
    public UISetSound UISound
    {
        get { return _soundUI; }
        set { _soundUI = value; }
    }



    private AimManagerUI _aimUI;

    public AimManagerUI AimUI
    {
        get { return _aimUI; }
        set { _aimUI = value; }
    }


    private int _score = 0;
    [SerializeField] private TMP_Text _pointsUI, _popupPointsUI;
    [SerializeField] private GameObject _popupPoints;


    public TMP_Text TextReference
    {
        get { return _pointsUI; }
        set { _pointsUI = value; }
    }
    public void AddScore(int amount)
    {
        _score += amount;

        if (_popupScoreCoroutine != null)
            StopCoroutine(_popupScoreCoroutine);
        _popupScoreCoroutine = StartCoroutine(ShowPopup(amount));

        if (_scoreCoroutine == null)
            _scoreCoroutine = StartCoroutine(AnimateScore());
    }

    private int _displayedScore = 0;
    private Coroutine _scoreCoroutine, _popupScoreCoroutine;

    private IEnumerator ShowPopup(int amount)
    {

        float fadeInDuration = 0.25f;
        float fadeInTimer = 0f;

        while (fadeInTimer < fadeInDuration)
        {
            fadeInTimer += Time.deltaTime;
            _popupPointsUI.alpha = Mathf.Lerp(1f, 0f, fadeInTimer / fadeInDuration);
            yield return null;
        }

        _popupPointsUI.alpha = 1f;
        TextReference.ForceMeshUpdate();
        int lastCharIndex = TextReference.textInfo.characterCount - 1;

        if (lastCharIndex >= 0)
        {
            var charInfo = TextReference.textInfo.characterInfo[lastCharIndex];

            Vector3 charPos = charInfo.bottomRight;
            float charHeight = charInfo.ascender - charInfo.descender;
            Vector3 verticalOffset = new Vector3(0f, charHeight * 0.35f, 0f);


            Vector3 worldPos = TextReference.transform.TransformPoint(charPos + verticalOffset);

            Vector3 offset = new Vector3(5f, 0f, 0f);

            _popupPointsUI.rectTransform.position = worldPos + offset;
        }



        _popupPointsUI.text = $"+${amount}";

        _popupPointsUI.gameObject.SetActive(true);

        float bumpDuration = 0.3f;
        float timer = 0f;
        Vector3 originalScale = Vector3.one;
        Vector3 enlargedScale = originalScale * 1.3f;

        while (timer < bumpDuration)
        {
            timer += Time.deltaTime;
            float t = timer / bumpDuration;
            float bump = Mathf.Sin(t * Mathf.PI);
            _popupPointsUI.rectTransform.localScale = Vector3.Lerp(originalScale, enlargedScale, bump);
            yield return null;
        }
        _popupPointsUI.rectTransform.localScale = originalScale;

        yield return new WaitForSeconds(0.5f);

        float fadeOutDuration = 0.25f;
        float fadeOutTimer = 0f;

        while (fadeOutTimer < fadeOutDuration)
        {
            fadeOutTimer += Time.deltaTime;
            _popupPointsUI.alpha = Mathf.Lerp(1f, 0f, fadeOutTimer / fadeOutDuration);
            yield return null;
        }
        _popupPointsUI.gameObject.SetActive(false);

        _scoreCoroutine = StartCoroutine(AnimateScore());
    }

    private IEnumerator AnimateScore()
    {
        while (_displayedScore < _score)
        {
            int step = Mathf.Max(1, (_score - _displayedScore) / 10);
            _displayedScore += step;

            if (_displayedScore > _score)
                _displayedScore = _score;

            TextReference.text = $"${_displayedScore}";
            yield return new WaitForSeconds(0.01f);
        }

        _scoreCoroutine = null;
    }

    public string transAnimation;
    public Animator animator;

    private IEnumerator PlayAnimationAfterLoad()
    {
        yield return null;

        if (animator != null && !string.IsNullOrEmpty(transAnimation))
        {
            animator.Play(transAnimation);
        }

    }

    private IEnumerator ClearInputBuffer()
    {
        yield return null;
        Input.ResetInputAxes();
    }

    private bool isMenuON;
    private void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        isMenuON = false;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Limpia los inputs 
        StartCoroutine(ClearInputBuffer());
    }
    private void Pause()
    {
        pauseMenu.SetActive(true);
        StartCoroutine(EnableMenuSafely());
        Time.timeScale = 0f;
        isMenuON = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

    }
    private IEnumerator EnableMenuSafely()
    {
        yield return null; // Espera 1 frame 
        Time.timeScale = 0f;
        isMenuON = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

}