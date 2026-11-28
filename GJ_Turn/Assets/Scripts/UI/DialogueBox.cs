using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DialogueBox : MonoBehaviour
{
    public static DialogueBox Instance {get ;private set;}

    //对话面板
    [Header("对话框UI")]
    public GameObject dialoguePanel;

    //面板上的两个文本
    public TMP_Text interactNameText ; //物品名
    public TMP_Text interactTextText ; //物品介绍

    //要关闭的面板
    public GameObject HUD;
    public GameObject Inventory;
    public GameObject InteractCheck;
    public GameObject Interactor;
    public Rigidbody2D rbPlayer ;

    // 是否正在对话
    private bool isDialogue = false;

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
    private void Start()
    {
        // 游戏开始时隐藏对话框
        dialoguePanel.SetActive(false);
    }

    private IEnumerator WaitForClick()
    {
        //等待一帧，避免把触发交互的点击当成下一句对话的点击，修复了第一句对话只有一帧的问题
        yield return null;

        while (isDialogue)
        {
            Debug.Log("正在对话");
            // 检测鼠标左键是否按下
            if (Input.GetKeyDown(KeyCode.Space))
            {
                break;
            }
            // 等待下一帧
            yield return null;
        }
        Debug.Log("退出对话");
    }

    //第一个参数是物品名，第二个是交互文本，第三个是页数
    public IEnumerator SetDialogueBox(string[] _interactName,string[] _interactText ,int n )
    {
        CloseAllUI();
        for(int i = 0 ; i < n ; i ++ )
        {
            interactNameText.text = _interactName[i];
            interactTextText.text = _interactText[i];
            yield return StartCoroutine(WaitForClick());
            Debug.Log($"结束第{i}句话");
        }
        OpenAllUI();

    }

    private void CloseAllUI()
    {
        Debug.Log("开始对话");
        isDialogue = true;
        dialoguePanel.SetActive(true);
        rbPlayer.constraints = RigidbodyConstraints2D.FreezeAll;
        InteractCheck.SetActive(false);
        Interactor.SetActive(false);
        HUD.SetActive(false);
        Inventory.SetActive(false);
    }
    private void OpenAllUI()
    {
        Debug.Log("结束对话");
        isDialogue = false;
        dialoguePanel.SetActive(false);
        rbPlayer.constraints = RigidbodyConstraints2D.None;
        InteractCheck.SetActive(true);
        Interactor.SetActive(true);
        HUD.SetActive(true);
        Inventory.SetActive(true);
    }
}