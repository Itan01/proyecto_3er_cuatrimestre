using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class VentTimelineTrigger : MonoBehaviour
{
    [Header("Timeline")]
    [SerializeField] private PlayableDirector introTimeline;

    [Header("Visual Actor")]
    [SerializeField] private GameObject timelinePlayer;

    private PlayerManager player;
    private CameraManager camManager;

    private bool inside;
    private bool played;

    private void Awake()
    {
        camManager = GameManager.Instance.CameraReference
            .GetComponent<CameraManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerManager pm))
        {
            player = pm;
            inside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerManager>())
        {
            inside = false;
            player = null;
        }
    }

    private void Update()
    {
        if (!inside || played || player == null)
            return;

        if (!GameManager.Instance.FirstTimeReference.gameObject.activeInHierarchy)
            return;

        if (!player.IsCrouching())
            return;

        PlayTimeline();
    }

    private void PlayTimeline()
    {
        played = true;

        GameManager.Instance.FirstTimeReference.gameObject.SetActive(false);

        player.ForceCrouch(false);

        player.SetIfPlayerCanMove(false);

        if (camManager != null)
            camManager.FreezeCam(true);

        timelinePlayer.transform.SetPositionAndRotation(
            player.transform.position,
            player.transform.rotation
        );

        timelinePlayer.SetActive(true);

        introTimeline.Play();
    }

    public void OnIntroTimelineFinished()
    {
        Rigidbody rb = player.GetRb();

        player.SetIfPlayerCanMove(false);

        rb.isKinematic = false;

        player.ResetPhysicsState();

        player.transform.SetPositionAndRotation(
            timelinePlayer.transform.position + Vector3.up * 0.1f,
            timelinePlayer.transform.rotation
        );

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        timelinePlayer.SetActive(false);

        if (camManager != null)
            camManager.FreezeCam(false);

        player.ForceCrouch(false);

        player.SetIfPlayerCanMove(true);

    }

}