using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioSourcePool : MonoBehaviour
{
    public enum SfxId
    {
        None = 0,//无音效
        // ── A. 玩家核心 ──────────────────────────────
        PlayerStep      = 10,   // 脚步
        FlashOn         = 11,   // 手电开（机械"咔"）
        FlashOff        = 12,   // 手电关（闷"嗒"）
        FlashBurst      = 13,   // 强光爆闪
        BatteryEmpty    = 14,   // 电池耗尽（HUD 抖动提示）

        // ── B. 虚化状态 ──────────────────────────────
        PhaseIn         = 20,   // 实体 → 虚化
        PhaseOut        = 21,   // 虚化 → 实体
        Tinnitus        = 22,   // 耳鸣（随虚化值反向，循环）

        // ── C. 交互 ─────────────────────────────────
        DoorOpen        = 30,   // 门开
        DoorLocked      = 31,   // 门锁拒绝（"开/拒绝 + 提示音"）
        DrawerOpen      = 32,   // 抽屉/柜子（初次、二次共用）
        Pick            = 33,        // 拾取
        ObjectPhaseOut  = 34,   // 虚化物品显形（必须对齐手电 1.2s）
        MirrorCompelete = 35,   // 镜子照射 2s 完成
        InPhased        = 36 ,//在虚化物品中被虚化

        // ── D. 床 ───────────────────────────────────
        BedRest         = 50,   // 小憩（重置手电实体化物）
        BedSleep        = 51,   // 入睡（进入下一轮）
        BedDeath        = 52,   // 虚化态上床 → 消散（一声闷响，之后全静）

        // ── E. 敌人 ─────────────────────────────────
        BigChaseLoop    = 60,   // 大怪追逐（循环）
        BigStun         = 61,   // 大怪被强光眩晕 3s

        // ── F. 轮次与叙事 ────────────────────────────
        WakeUp          = 70,   // 每轮醒来（四轮共用同一段）
        RoundTransition = 71,   // 切轮（0.5s 内完成，不要长转场）
        MachineInsert   = 72,   // 碎片入槽 ×4（音高逐次升高）
        MachineStart    = 73,   // 实体化机启动
        MachineLoop     = 74,   // 30s 读条（循环）
        MachineInterrupt= 75,   // 读条被打断
        EndingFlash     = 76,   // 白光
        EndingSilence   = 77,   // 黑屏后的完全静音

        SFXend          = 78,   //最后一个音效，用来判断用哪个音频混合器

        // ── H. UI ──────────────────────────────────
        UiClick         = 90,   // 通用点击
        UiBackpackOpen  = 91,
        UiBackpackClose = 92,
        UiToast         = 93,   // 提示文字弹出

        UIend           = 94    //最后一个ui音效
    }
    
    [Header("对象池初始数量设置")]
    [SerializeField] int poolSize = 5;

    [Header("音频管理器设置")]
    [SerializeField] AudioMixerGroup SFXGroup;
    
    [SerializeField] AudioMixerGroup UIGroup;

    // 保存对象池中的所有音频播放器
    private List<AudioSource> audioSources = new List<AudioSource>();

    private void Start()
    {
        // 游戏开始时，初始化对象池
        InitializePool();
    }

    private void InitializePool()
    {
        // 提前创建指定数量的音频播放器
        for (int i = 0; i < poolSize; i++)
        {
            CreateAudioSource();
        }
    }

    private AudioSource CreateAudioSource()
    {
        // 创建一个新的空物体
        GameObject audioObject = new GameObject("AudioPlayer");

        // 将播放器放在对象池物体下面，方便管理
        audioObject.transform.SetParent(transform);

        // 添加 AudioSource 组件
        AudioSource source = audioObject.AddComponent<AudioSource>();

        // 初始时不启用播放器
        audioObject.SetActive(false);

        // 将播放器保存到对象池列表
        audioSources.Add(source);

        return source;
    }

    private AudioSource GetAudioSource()
    {
        // 遍历对象池，寻找没有启用的播放器
        foreach (AudioSource source in audioSources)
        {
            if (!source.gameObject.activeInHierarchy)
            {
                return source;
            }
        }

        // 如果没有空闲播放器，就创建一个新的
        return CreateAudioSource();
    }

    private IEnumerator ReturnAfterPlay(AudioSource source, float playTime)
    {
        // 等待音效播放结束
        yield return new WaitForSeconds(playTime);

        // 停止播放
        source.Stop();

        // 清除之前的音频资源
        source.clip = null;

        // 恢复默认音调
        source.pitch = 1f;

        // 关闭播放器，表示归还对象池
        source.gameObject.SetActive(false);
    }
    
    public void PlaySound(AudioClip clip, SfxId id)
    {
        // 从对象池获取一个空闲播放器
        AudioSource source = GetAudioSource();

        // 设置音频输出混音组
        if (id < SfxId.SFXend)
        {
            source.outputAudioMixerGroup = SFXGroup;
        }
        else
        {
            source.outputAudioMixerGroup = UIGroup;
        }

        // 设置要播放的音频资源
        source.clip = clip;

        // 启用播放器
        source.gameObject.SetActive(true);

        // 播放音效
        source.Play();

        // 计算音效实际播放时间
        float playTime = clip.length;

        // 播放结束后，将播放器归还对象池
        StartCoroutine(ReturnAfterPlay(source, playTime));
    }

}
