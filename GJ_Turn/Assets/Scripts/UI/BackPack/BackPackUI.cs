using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;


public class BackPackUI : MonoBehaviour
{
    public static BackPackUI Instance {get ;private set;}

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
        for (int i = 0 ; i < this.transform.childCount ; i++) //添加到数组内
        {
            backPackItems[i] = transform.GetChild(i).gameObject;
        }
    }
    private GameObject[] backPackItems = new GameObject[6]; //六个空格

    //public Item Item; //测试用物体后面会加上交互系统


    void Start()
    {
        //this.gameObject.SetActive(false);
        
        //SetItem(Item); //测试用函数
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
    public void SetItem(Item item) //将前者（空格）中填入后者的数据
    {
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
        GameObject backPackIcon = backPackItems[0];
        while (backPackIcon.GetComponent<Image>().sprite == null)
        {
            backPackIcon = backPackItems[0];
        }
    }

}
