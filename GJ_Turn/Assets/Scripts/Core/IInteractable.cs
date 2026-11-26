using UnityEngine;

public interface IInteractable
{
    //用于交互检测的碰撞箱
    Collider2D Collider { get; }
    // 玩家与物体交互时执行
    void Interact(PlayerInteractor player);
}