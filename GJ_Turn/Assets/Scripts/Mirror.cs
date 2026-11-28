using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mirror : MonoBehaviour
{
    [SerializeField] private PlayerController.Direction mirrorDirection;
    public static GameObject player { get; private set; }
    public static PlayerController playerController { get; private set; }
    public static PlayerFadeController playerFadeController { get; private set; }

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            playerController = player.GetComponent<PlayerController>();
            playerFadeController = player.GetComponent<PlayerFadeController>();
        }
    }

    private void Update()
    {
        if (!playerController.isFlashOn)
        {
            return;
        }
        if (mirrorDirection != playerController.playerDirection)
        {
            return;
        }
        Vector2 selfPos = transform.position;
        Vector2 playerPos = player.transform.position;
        Vector2 toTarget = selfPos - playerPos;
        float dist = toTarget.magnitude;
        if (dist > 8f)
        {
            return;
        }
        float targetAngle = Mathf.Atan2(toTarget.y, toTarget.x) * Mathf.Rad2Deg - 90f;
        if (Mathf.Abs(Mathf.DeltaAngle(playerController.GetCurrentFlashAngle(), targetAngle)) > 27.5f)
        {
            return;
        }
        playerFadeController.ChangeFadeProgress(-Time.deltaTime / 2.4f);
    }
}