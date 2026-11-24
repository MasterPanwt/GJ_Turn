using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPoolTest : MonoBehaviour
{
    [Header("对象池")]
    public AudioSourcePool audioPool;
    [Header("BGM")]
    public SettingBGM BGMAudio;


    [Header("测试音效")]
    public AudioClip testClip;
    public AudioClip testClip2;

    private void Update()
    {
        // 按下 K 键，测试播放音效
        if (Input.GetKeyDown(KeyCode.K))
        {
            if (audioPool != null && testClip != null)
            {
                Debug.Log("按下K键播放第一个音频");
                // 从对象池播放音效
                audioPool.PlaySound(testClip,AudioSourcePool.SfxId.PlayerStep);
            }
        }
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (audioPool != null && testClip != null)
            {
                Debug.Log("按下J键播放第二个音频");

                BGMAudio.PlaySound(testClip2);
            }
        }
    }
}
