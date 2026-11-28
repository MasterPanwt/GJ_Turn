using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour , IInteractable
{
    public Collider2D Collider { get; private set; }
    public void Awake()
    {
        Debug.Log("PickUp组件可以用");
        Collider = GetComponent<Collider2D>();
    }
    public void Interact(PlayerInteractor player)
    {
        
        StartCoroutine(InteractCoroutine());
        
    }
    private IEnumerator InteractCoroutine()
    {
        Debug.Log("触发接口了");
        string s1 = "物品名：钥匙";
        string s2 = "物品名：门";
        string s3 = "这个可以用来开门";
        string s4 = "这个可以需要用钥匙打开";
        string[] strings1 = {s1,s2};
        string[] strings2 = {s3,s4};
        yield return StartCoroutine(
            DialogueBox.Instance.SetDialogueBox(strings1, strings2, 2)
        );
        BackPackUI.Instance.SetItem(this.gameObject); //包含Destroy(gameObject);
    }
}
