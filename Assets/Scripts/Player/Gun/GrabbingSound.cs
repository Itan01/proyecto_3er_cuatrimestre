using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GrabbingSound : MonoBehaviour
{
    [SerializeField] private LayerMask _enviromentMask;
    private float _grabbingTimer = 3f;
    private float _sinceLastPlayed = 3f;
    
    private void Update()
    {
        transform.forward = Camera.main.transform.forward;
        if(_sinceLastPlayed >= _grabbingTimer) 
        {
            AudioStorage.Instance.GrabbingSound();
            _sinceLastPlayed = 0f;
        }
        else
        {
            _sinceLastPlayed += Time.deltaTime;
        }
    }
    private void OnTriggerStay(Collider Sound)
    {
        if (Sound.TryGetComponent<AbstractSound>(out AbstractSound Script))
        {
            if (Script.HasLineOfVision(_enviromentMask, GameManager.Instance.PlayerReference.transform.position + new Vector3(0,1.25f,0)))
            {
                Script.PlayerCanCatchIt(true);
                Script.SetTarget(GameManager.Instance.PlayerReference.transform, 20.0f);
            }
        }
    }

    public void Desactivate()
    {
        if(gameObject.activeSelf)
        StartCoroutine(SetActivateFalse());
        _sinceLastPlayed = 2.9f;
    }
    private IEnumerator SetActivateFalse()
    {
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
}
