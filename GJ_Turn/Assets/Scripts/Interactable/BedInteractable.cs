using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BedInteractable : MonoBehaviour, IInteractable
{
    public void Interact(PlayerInteractor player)
    {
        Debug.Log("Interacted with bed");//占位脚本
    }
}
