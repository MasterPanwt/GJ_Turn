using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public static GameObject player { get; private set; }
    public static PlayerController playerController { get; private set; }
    public int fadeNum { get; private set; }

    private void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            playerController = player.GetComponent<PlayerController>();
        }
        playerController.AddTarget(this.gameObject);
    }

    private void OnDestroy()
    {
        if (playerController != null)
        {
            playerController.DelTarget(this.gameObject);
        }
    }

    public void ChangeFadeNum(int delta)
    {
        fadeNum = Mathf.Clamp(fadeNum + delta, 0, 100);
    }
}