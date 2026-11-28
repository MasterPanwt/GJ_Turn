using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class XuHuaZhiUI : MonoBehaviour
{
    public Image XHZ_Image { get; private set; }
    
    public float XHZ_Value { get; private set; }

    public float healDuration = 2f;

    bool isHeal;

    void Start()
    {
        XHZ_Image = transform.GetChild(0).gameObject.GetComponent<Image>();
        XHZ_Value = XHZ_Image.fillAmount ;
    }

     public IEnumerator HealAnimation(float targetFill)
    {
        targetFill = Mathf.Clamp(targetFill , 0 , 1);

        // 记录回血开始时的血条比例
        float startFill = XHZ_Image.fillAmount;


        // 记录已经经过的时间
        float timer = 0f;

        while (timer < healDuration)
        {
            // 累计时间
            timer += Time.deltaTime;

            // 根据进度逐渐增加血条
            XHZ_Image.fillAmount = Mathf.Lerp(
                startFill,
                targetFill,
                timer / healDuration //时间
            );
            // 等待下一帧
            yield return null;
            
        }
        isHeal = false;
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q) && !isHeal)
        {
            Debug.Log("开始恢复");
            isHeal = true;
            StartCoroutine(HealAnimation(1));
        }
        else if(Input.GetKeyDown(KeyCode.E) && !isHeal)
        {
            Debug.Log("开始掉血");
            isHeal = true;
            StartCoroutine(HealAnimation(0));

        }
    }
}
