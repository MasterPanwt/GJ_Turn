using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class Item : MonoBehaviour, IInteractable
{
    [HideInInspector]
    public string ItemName ;
    public Collider2D Collider { get; private set; }
    [Header("物品简介")]
    [TextArea(3,4)] 
    public string Data ;

    [HideInInspector]
    public Sprite sprite;

    void Awake()
    {
        gameObject.layer = LayerMask.NameToLayer("Interactable");
        ItemName = this.name;
        Collider = GetComponent<Collider2D>();
        sprite = gameObject.GetComponent<SpriteRenderer>().sprite;
    }

    public void Interact(PlayerInteractor player)
    {
        BackPackUI.Instance.SetItem(this.gameObject);
        GameObject.Destroy(this.gameObject);
    }

}
