using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//场景退出
public class SceneExit : MonoBehaviour
{
    [Tooltip("需要过度新场景的名称")]
    public string newSceneName;
    void Start()
    {
        Debug.Log("这个脚本在起作用");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("触发了");
        if (other.CompareTag("Player"))
        {
            TransitionInternal();
        }
    }
    public void TransitionInternal()
    {
        SceneLoader.Instance.TransitionToScene(newSceneName);
    }
}
