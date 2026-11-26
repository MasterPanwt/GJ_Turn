using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedInteractable : MonoBehaviour, IInteractable
{
    public Collider2D Collider { get; private set; }
    public void Awake()
    {
        Collider = GetComponent<Collider2D>();
    }
    public void Interact(PlayerInteractor player)
    {
        Debug.Log("Interacted with bed");//占位脚本
    }
}
