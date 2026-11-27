using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//这是交互系统不行的时候备用的交互系统
public class TestInteact : MonoBehaviour
{
    public IInteractable interactable; //可交互的组件
    public GameObject player;
    public GameObject item;
    void Start()
    {
        interactable = item.GetComponent<Item>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            interactable.Interact(player.GetComponent<PlayerInteractor>());
        }
    }
}
