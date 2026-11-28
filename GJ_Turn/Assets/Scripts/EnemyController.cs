using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public static GameObject player { get; private set; }
    public static PlayerController playerController { get; private set; }

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            playerController = player.GetComponent<PlayerController>();
            playerController.onDazzle.AddListener(CheckDazzle);
        }
    }

    private void OnEnable()
    {
        if (playerController != null)
        {
            playerController.onDazzle.AddListener(CheckDazzle);
        }
    }

    private void OnDisable()
    {
        if (playerController != null)
        {
            playerController.onDazzle.AddListener(CheckDazzle);
        }
    }

    private void CheckDazzle()
    {
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
        Destroy(gameObject);
    }
}