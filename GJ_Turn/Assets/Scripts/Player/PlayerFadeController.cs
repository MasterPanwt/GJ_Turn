using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFadeController : MonoBehaviour
{
    public int fadeNum = 0;
    private float fadeProgress = 0f;
    private SpriteRenderer sr;
    private CapsuleCollider2D cc;
    private PlayerInteractor pi;
    private readonly List<FadeController> fadeControllers = new List<FadeController>();

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cc = GetComponent<CapsuleCollider2D>();
        pi = GetComponentInChildren<PlayerInteractor>();
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
            if (fc.tag == "SpecialFadeObject" && fc.progress < 1f)
            {
                ChangeFadeProgress(Time.deltaTime / 2.4f);
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
            pi.canInteract = true;
        }
        else if (fadeNum < 100)
        {
            sr.sortingOrder = 6;
            sr.color = new Color32(255, 255, 255, 128);
            cc.isTrigger = true;
            pi.canInteract = false;
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

    public void ChangeFadeProgress(float val)
    {
        fadeProgress = Mathf.Clamp(fadeProgress + val, 0f, 1f);
    }
}