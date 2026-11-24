using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SettingBGM : MonoBehaviour
{
    public enum BGMId
    {
        AmbRoomAir   ,   // 室内空气声（卧室/客厅）
        AmbKitchen   ,   // 厨房冰箱低鸣
        AmbBathroom  ,   // 浴室滴水/管道
    }

    private AudioSource source;

    [SerializeField] AudioMixerGroup BGMGroup;

    void Start()
    {
        source = transform.GetComponent<AudioSource>();

    }
    public void PlaySound(AudioClip clip)
    {
        // 设置音频输出混音组
        source.outputAudioMixerGroup = BGMGroup;

        // 设置要播放的音频资源
        source.clip = clip;

        source.loop = true;

        // 播放音效
        source.Play();
    }
}
