using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{
    public Transform spawnPoint;
    public Animator animator;
    private PlayerController player;

    private void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    public void HitByStone()
    {
        player.canThrow = false; // 玩家不能投掷
        animator.SetTrigger("Destroyed"); 
        // 假设动画播放时间为2秒
        Invoke("RespawnCar", 2f);
    }

    void RespawnCar()
    {
        transform.position = spawnPoint.position;
        player.canThrow = true; 
    }
}
