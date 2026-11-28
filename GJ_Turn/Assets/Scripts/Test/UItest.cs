using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UItest : MonoBehaviour
{
    SpriteRenderer sr;
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        SetHighlight();
    }
    void SetHighlight()
    {
        sr.color = Color.black;
    }
}
