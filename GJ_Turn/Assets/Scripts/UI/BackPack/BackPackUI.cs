using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class BackPackUI : MonoBehaviour
{
    public static BackPackUI Instance {get ;private set;}

    public GameObject[] backPackItems = new GameObject[6]; //六个空格

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
    }
    
    //public Item Item; //测试用物体后面会加上交互系统


    void Start()
    {
        SwapItem();
        //this.gameObject.SetActive(false);
        
        //SetItem(Item); //测试用函数
    }

    void Update()
    {
        
    }

    //遍历位置然后找到空位(测试成功)
    private GameObject GetIcon()
    {
        foreach (GameObject backPackIcon in backPackItems)
        {
            if (backPackIcon.GetComponent<Image>().sprite == null)
            {
                return backPackIcon;
            }
        }
        Debug.Log("背包已满");
        return backPackItems[0];
    }
    //将物品信息填入空位，结合交互函数实现(测试成功)
    //填入有物品名，介绍，图片
    public void SetItem(GameObject gameObjectItem) //将前者（空格）中填入后者的数据
    {
        Item item = gameObjectItem.GetComponent<Item>();
        GameObject back = GetIcon(); //获取空格
        ItemInBackPack myBackPackItem = back.GetComponent<ItemInBackPack>(); //获取空格上的空格脚本
        //编辑空格脚本数据
        myBackPackItem.sprite = item.sprite; 
        myBackPackItem.data = item.Data;
        myBackPackItem.name = item.name;
        //将空格的图片换成物品的
        back.GetComponent<Image>().sprite = item.sprite;
    }

    //半成品
    private void SwapItem()
    {
        for (int i = 0 ; i < 5 ; i ++)
        {
            if (backPackItems[i].GetComponent<Image>().sprite == null && backPackItems[i+1].GetComponent<Image>().sprite != null )
            {
                Debug.Log("交换");
                Vector3 temp = backPackItems[i].transform.position;
                backPackItems[i].transform.position = backPackItems[i + 1].transform.position ;
                backPackItems[i + 1 ].transform.position = temp ;
                GameObject tempObj = backPackItems[i];
                backPackItems[i] = backPackItems[i + 1];
                backPackItems[i + 1] = tempObj; 
            }
        }
    }

}
