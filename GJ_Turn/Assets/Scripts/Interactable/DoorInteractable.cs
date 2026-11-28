using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorInteractable : MonoBehaviour, IInteractable
{
    public Collider2D Collider { get; private set; }
    private Transform open;
    private Transform close;
    private bool isClosed = true;
    public void Awake()
    {
        Collider = GetComponent<Collider2D>();
        if(!Collider)
        {
            Debug.LogWarning("没有碰撞箱");
        }
        open = transform.GetChild(0);
        close = transform.GetChild(1);
        open.gameObject.SetActive(false);
        close.gameObject.SetActive(true);
    }
    public void Interact(PlayerInteractor player)
    {
        if(isClosed)
        {
            Debug.Log("Open");
            open.gameObject.SetActive(true);
            close.gameObject.SetActive(false);
            isClosed = false;
            Collider.isTrigger = true;
        }
        else
        {
            Debug.Log("Close");
            open.gameObject.SetActive(false);
            close.gameObject.SetActive(true);
            isClosed =true;
            Collider.isTrigger = false;
        }
    }
}