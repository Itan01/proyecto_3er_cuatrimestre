using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [Header("Pared a activar")]
    [SerializeField] private GameObject wallObject;
    [SerializeField] private float riseHeight = 7f;
    [SerializeField] private float riseSpeed = 3f;

    private bool isWallActive = false;
    private Vector3 initialWallPos;
    private Vector3 targetWallPos;

    private Coroutine currentRoutine;
    private RoomManager _room;

    private bool _playerOnPlate = false;

    private void Start()
    {
        if (wallObject == null)
        {
            Debug.LogWarning("No hay pared asignada.");
            return;
        }

        initialWallPos = wallObject.transform.position;
        targetWallPos = initialWallPos + Vector3.up * riseHeight;

        wallObject.transform.position = initialWallPos;
        //wallObject.SetActive(false);

        _room = GetComponentInParent<RoomManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (!_playerOnPlate)
        {
            _playerOnPlate = true;
            ToggleWall(); 
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        _playerOnPlate = false; 
    }

    private void ToggleWall()
    {
        isWallActive = !isWallActive;


        if (currentRoutine != null)
            StopCoroutine(currentRoutine);

        currentRoutine = StartCoroutine(MoveWall(isWallActive));
        _room.ResetPath();
    }

    private IEnumerator MoveWall(bool activate)
    {
        if(activate)
            AudioStorage.Instance.OpenDoorSound();
        else
            AudioStorage.Instance.CloseDoorSound();
        wallObject.SetActive(true);
        float t = 0f;
        Vector3 startPos = wallObject.transform.position;
        Vector3 endPos = activate ? targetWallPos : initialWallPos;

        while (t < 1f)
        {
            t += Time.deltaTime * riseSpeed;
            wallObject.transform.position = Vector3.Lerp(startPos, endPos, t);
            yield return null;
        }

        if (!activate)
        {
            //wallObject.SetActive(false);
        }
    }
}
