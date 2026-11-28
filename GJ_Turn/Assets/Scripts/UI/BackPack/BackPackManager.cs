using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPackManager : MonoBehaviour
{
    public GameObject backPackPanel;
    void Start()
    {
        CloseBackPack();
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            if(!backPackPanel.activeInHierarchy)
            {
                Debug.Log("打开背包");
                OpenBackPack();
            }
            else
            {
                CloseBackPack();
            }
            
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            CloseBackPack();
        }
    }

    private void OpenBackPack()
    {
        backPackPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    private void CloseBackPack()
    {
        backPackPanel.SetActive(false);
        Time.timeScale = 1f;
    }

}
