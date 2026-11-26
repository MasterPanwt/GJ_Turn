using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeController : MonoBehaviour
{
    public static GameObject player { get; private set; }
    public static PlayerController playerController { get; private set; }

    SpriteRenderer sr;
    [SerializeField] private float fullduration = 1.2f;
    [Range(0f,1f)] public float progress = 0f;

    Coroutine _co;
    Collider2D _collider;

    private void Awake()
    {
        if(!_collider)_collider = GetComponent<Collider2D>();
        if(!_collider)
        {
            Debug.LogError($"[{name}] 没有 Collider2D", this);
            this.enabled = false;
        }
        if(!sr)sr = GetComponent<SpriteRenderer>();
        if(!sr)
        {
            Debug.LogError($"[{name}] 没有 SpriteRenderer", this);
            this.enabled = false; 
        }
    }
    private void Start()
    {
        if (player == null)
        {
            player = GameObject.Find("Player");
            playerController = player.GetComponent<PlayerController>();
        }
        playerController.AddTarget(this.gameObject);
        Apply(progress);
    }

    private void OnDestroy()
    {
        if (playerController != null)
        {
            playerController.DelTarget(this.gameObject);
        }
    }
    public void Rampto(float to)
    {
        if (_co != null) StopCoroutine(_co);
        _co = StartCoroutine(Run(to));

    }

    IEnumerator Run(float to)//通过携程实现按时间改变不透明度
    {
        float from = progress;
        float travel = Mathf.Abs( progress - to);
        if(travel < 0.0001f)yield break;//到达目标时候退出协程

        float elapsed = 0f;//已经过去的时间
        float need = travel * fullduration;//需要的时间

        while (elapsed < need)
        {
            elapsed += Time.deltaTime;
            progress = Mathf.Lerp(from, to, elapsed / need);//按时间改变不透明度
            Apply(progress);
            yield return null;
        }
        progress = to;
        Apply(to);
    }
    public void Apply(float val)//改变不透明度
    {
        float a = Mathf.Lerp(128f / 255f, 1f, Mathf.Clamp01(val));
        Color c = sr.color;
        c.a = a;
        sr.color = c;
    }
}