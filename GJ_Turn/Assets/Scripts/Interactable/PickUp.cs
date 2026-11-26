using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour , IInteractable
{
    public Collider2D Collider { get; private set; }
    public void Awake()
    {
        Collider = GetComponent<Collider2D>();
    }
    public void Interact(PlayerInteractor player)
    {
        Debug.Log("触发接口了");
        gameObject.SetActive(false);
    }
}
