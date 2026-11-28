using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskInteractable : MonoBehaviour, IInteractable
{
    public Collider2D Collider { get; private set; }
    public void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
        Collider = GetComponent<Collider2D>();
    }
    private bool isInteracted { get; set; } = false; 
    public void Interact(PlayerInteractor player)
    {
        if (!isInteracted)
        {
            Debug.Log("捡起了钥匙");
            isInteracted = true;
            
        }
        else Debug.Log("已经捡过钥匙了");
    }
}
