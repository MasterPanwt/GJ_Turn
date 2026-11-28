using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
public class ScreenFader : MonoBehaviour
{
    
    public static ScreenFader Instance {get ;private set;}

    public CanvasGroup faderCanvasGroup ;
    public float fadeDuration = 1f ;
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
        DontDestroyOnLoad(gameObject);
    }

    //淡入场景
    public IEnumerator FadeSceneIn()
    {
        yield return StartCoroutine(Fade(0f, faderCanvasGroup)); //将透明度调为0
        faderCanvasGroup.gameObject.SetActive(false); //禁用淡入淡出
    }
    //淡出场景
    public IEnumerator FadeSceneOut()
    {
        faderCanvasGroup.gameObject.SetActive(true); 
        yield return StartCoroutine(Fade(1f, faderCanvasGroup)); //将透明度调为0
        
        
    }

    //淡入出实现
    public IEnumerator Fade(float finalAlpha , CanvasGroup canvasGroup)
    {
        //当最后透明度变到最终时停下，第二个参数时持续时间，Wait函数是当执行完后才返回
        yield return canvasGroup.DOFade(finalAlpha,fadeDuration).WaitForCompletion();
    }

}
