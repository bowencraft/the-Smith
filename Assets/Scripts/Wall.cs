using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Wall : MonoBehaviour
{
    public int durability = 9;
    private Animator ar;
    public GameObject WinObject;
    public GameObject LoseObject;
    public AudioSource chargeAudioSource; // 投掷机蓄力的音频源
    public AudioClip EndSounds;
    public GameObject sceneFader;

    private void Start()
    {
        ar = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        durability -= damage;
        if (durability <= 0)
        {
            WinObject.SetActive(false);
            LoseObject.SetActive(true);
            chargeAudioSource.clip = EndSounds;
            chargeAudioSource.Play();

            Invoke("SwitchScene", 4f);
        }
        ar.SetInteger("wall_health", durability);
    }

    public void SwitchScene()
    {
        sceneFader.GetComponent<SceneFader>().FadeToScene("Scene_Start");
        //SceneManager.LoadScene("Scene_Start");
    }
}
