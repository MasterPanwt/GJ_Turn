using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class PlayerInteractor : MonoBehaviour
{ 
    public TMP_Text interactPrompt; //屏幕上的交互提示文字
    IInteractable interactable; //可交互的组件

    // 尝试与视线中心的物体交互
    private void OnTriggerStay2D(Collider2D other)
    {
        // 获取物体上的交互组件
        interactable =other.GetComponentInParent<IInteractable>();
    }

    void Update()
    {
        // 如果物体支持交互，就执行交互方法
        if (interactable != null)
        {
            interactPrompt.text = "E";
            if (Input.GetKeyDown(KeyCode.E))
            {
                interactable.Interact(this);
            }
        }
        else {interactPrompt.text = null;}
    }

}
