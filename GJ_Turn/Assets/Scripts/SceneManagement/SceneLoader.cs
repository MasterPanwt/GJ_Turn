using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get ; private set;}
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject); //加载新场景不销毁这个

    }

    //加载新场景

    public void TransitionToScene(string sceneName)
    {

        StartCoroutine(TransitionCoroutine(sceneName));
        
    }

    public IEnumerator TransitionCoroutine(string newSceneName)
    {
        //保存所有持久化数据

        //淡出场景

        //异步加载新场景
        yield return SceneManager.LoadSceneAsync(newSceneName); //新方法

        //加载所有数据

        //获取场景过度的位置 

        //设置进入游戏的位置

        //淡入新场景

        
    }
}
