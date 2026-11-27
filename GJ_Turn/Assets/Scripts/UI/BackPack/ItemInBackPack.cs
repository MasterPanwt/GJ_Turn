using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ItemInBackPack : MonoBehaviour
{
    [HideInInspector]
    public string itemName ;

    [Header("介绍")]
    [TextArea(3,4)] 
    public string data ;

    [HideInInspector]
    public Sprite sprite;

    public GameObject introPanel ;

    public GameObject submitBotton;

    [HideInInspector]
    public bool isSubmitting ;

    void Awake()
    {
        isSubmitting = false;

        itemName = this.name;
        
        sprite = gameObject.GetComponent<Image>().sprite;
    }

    
    

    
    //1. 无法使用  2. 进行对应交互

    //否：点击弹出介绍面板
    public void OpenIntroPanal()
    {
        if(isSubmitting)//判断是不是需要用的时候打开
        {
            submitBotton.SetActive(true);//是： 弹出是否确实使用
        }
        else
        {
            submitBotton.SetActive(false);
        }
        if(sprite != null)//如果点击空就不起作用
        {
            introPanel.SetActive(true);
            TMP_Text IntroText = introPanel.GetComponentInChildren<TMP_Text>();
            IntroText.text = this.GetComponentInChildren<ItemInBackPack>().data;
        }
        else
        {
            Debug.Log("这个格子是空");
        }
        
    }
}
