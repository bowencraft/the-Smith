using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject stonePrefab;
    public Transform throwPoint;
    public float throwForce = 200f;
    public float angle = 1.5f;
    public bool isPlayer1;
    private float chargeTime = 0f;
    Animator animator; // 添加玩家的动画组件
    public bool canThrow = true; // 确定玩家是否可以投掷石头

    public AudioSource chargeAudioSource; // 投掷机蓄力的音频源



    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
    }

    void Update()
    {
        //if (!canThrow) return; // 如果玩家不能投掷，直接返回

        if (isPlayer1)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                animator.SetTrigger("prepare");
                StartCharging();
            }

            if (Input.GetKey(KeyCode.A))
            {
                chargeTime += Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.A))
            {
                StopCharging();
                animator.SetTrigger("throw");
                ThrowStone(chargeTime);
                chargeTime = 0f;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                animator.SetTrigger("prepare");
                StartCharging();
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                chargeTime += Time.deltaTime;
            }
            if (Input.GetKeyUp(KeyCode.RightArrow))
            {
                StopCharging();
                animator.SetTrigger("throw");
                ThrowStone(chargeTime);
                chargeTime = 0f;
            }
        }
    }

    void ThrowStone(float charge)
    {
        GameObject stone = Instantiate(stonePrefab, throwPoint.position, Quaternion.identity);
        Rigidbody2D rb = stone.GetComponent<Rigidbody2D>();
        float direction = isPlayer1 ? 1f : -1f;
        rb.AddForce(new Vector2(direction * throwForce * charge, throwForce * charge * angle));
    }

    // 当石头击中玩家时调用此函数
    public void HitByStone()
    {
        canThrow = false; // 玩家不能投掷
        animator.SetTrigger("broke"); // 播放被击中的动画
        // 假设动画播放时间为2秒
        //Invoke("EnableThrow", 4f); // 2秒后玩家可以再次投掷
    }

    void StartCharging()
    {
        if (!chargeAudioSource.isPlaying)
        {
            chargeAudioSource.Play();
        }
    }

    void StopCharging()
    {
        if (chargeAudioSource.isPlaying)
        {
            chargeAudioSource.Stop();
        }
    }
}
