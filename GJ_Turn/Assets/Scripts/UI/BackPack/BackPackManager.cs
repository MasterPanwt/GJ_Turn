using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackPackManager : MonoBehaviour
{
    public GameObject backPackPanel;
    void Start()
    {
        backPackPanel.SetActive(false);
        DontDestroyOnLoad(this);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            backPackPanel.SetActive(true);
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            backPackPanel.SetActive(false);
        }
    }
}
