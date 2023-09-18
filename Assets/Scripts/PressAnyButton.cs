using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PressAnyButton : MonoBehaviour
{
    public AudioSource chargeAudioSource; // 投掷机蓄力的音频源
    public AudioClip EndSounds;
    public GameObject sceneFader;
    public GameObject tips;

    private bool visible = true;
    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && visible)
        {
            visible = false;
            Invoke("SwitchScene", 3f);
            tips.GetComponent<SpriteRenderer>().enabled = false;
            chargeAudioSource.clip = EndSounds;
            chargeAudioSource.Play();
        }
    }

    public void SwitchScene()
    {
        sceneFader.GetComponent<SceneFader>().FadeToScene("Scene_Game");
    }
}
