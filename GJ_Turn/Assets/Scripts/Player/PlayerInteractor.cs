using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;
using static UnityEngine.UI.Image;

public class PlayerInteractor : MonoBehaviour
{ 
    public GameObject interactPanel;
    //public TMP_Text interactPrompt; //屏幕上的交互提示文字
    IInteractable interactable; //可交互的组件
    [SerializeField] float radius = 0.5f;//交互半径
    LayerMask interactableMask;

    public bool canInterct;


    private void Awake()
    {
        interactableMask = LayerMask.GetMask("Interactable");
        canInterct = true ;//默认是可以交互
    }


    void Update()
    {
        IInteractable hit = TryGetInteractable();

        // 如果得到最近可交互物，就执行交互方法
        if (hit != null && canInterct)
        {
            OpenInteractPanel();
            if (Input.GetKeyDown(KeyCode.E))
            {
                hit.Interact(this);
            }
        }
        else {CloseInteractPanel();}
    }
    IInteractable TryGetInteractable()//尝试获取最近的可交互物
    {
        Collider2D[] _buffer = new Collider2D[16];
        IInteractable nearest = null;

        int n = Physics2D.OverlapCircleNonAlloc(transform.position, radius, _buffer, interactableMask);
        float bestSqr = float.MaxValue;
        Vector2 me = transform.position;
        for (int i = 0; i < n; i++)
        {
            if (!_buffer[i].TryGetComponent<IInteractable>(out IInteractable it)) continue;

            float sqr = ((Vector2)_buffer[i].transform.position - me).sqrMagnitude;
            if (sqr < bestSqr) { bestSqr = sqr; nearest = it; }
        }
        return nearest;
    }

    public void OpenInteractPanel()
    {
        interactPanel.SetActive(true);  
    }
    

    public void CloseInteractPanel()
    {
        interactPanel.SetActive(false);
    }
    

}
