using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteractable : MonoBehaviour, IInteractable
{
    public Collider2D Collider { get; private set; }
    public void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
        Collider = GetComponent<Collider2D>();
    }
    public void Interact(PlayerInteractor player)
    {
        BackPackUI.Instance.SetItem(this.gameObject);
    }
}
