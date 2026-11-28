using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFadeController : MonoBehaviour
{
    public int fadeNum = 0;
    private float fadeProgress = 0f;
    private SpriteRenderer sr;
    private CapsuleCollider2D cc;
    private readonly List<FadeController> fadeControllers = new List<FadeController>();

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        for (int i = fadeControllers.Count - 1; i >= 0; i--)
        {
            FadeController fc = fadeControllers[i];
            if (fc == null)
            {
                fadeControllers.RemoveAt(i);
                continue;
            }
            if (fc.progress < 1f)
            {
                fadeProgress = Mathf.Clamp(fadeProgress + Time.deltaTime / 10f, 0f, 1f);
            }
        }
    }

    private void LateUpdate()
    {
        fadeNum = Mathf.FloorToInt(Mathf.Lerp(0, 100, fadeProgress));
        if (fadeNum <= 50)
        {
            sr.sortingOrder = 5;
            sr.color = new Color32(255, 255, 255, 255);
            cc.isTrigger = false;
        }
        else if (fadeNum < 100)
        {
            sr.sortingOrder = 6;
            sr.color = new Color32(255, 255, 255, 128);
            cc.isTrigger = true;
        }
        else
        {
            Debug.Log("FadeNum greater than 100!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        FadeController fc = collider.GetComponent<FadeController>();
        if (fc != null)
        {
            fadeControllers.Add(fc);
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        FadeController fc = collider.GetComponent<FadeController>();
        if (fc != null)
        {
            fadeControllers.Remove(fc);
        }
    }
}