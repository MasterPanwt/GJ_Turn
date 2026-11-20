using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUp : MonoBehaviour , IInteractable
{
    public void Interact(PlayerInteractor player)
    {
        Debug.Log("触发接口了");
        gameObject.SetActive(false);
    }
}
