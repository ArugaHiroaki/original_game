using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    [SerializeField] AudioClip[] backGroundMusic;
    AudioSource audioSource;

    [SerializeField] GameObject player;
    PlayerController playerController;
    int currentStageNum;
    int musicNum;
    float[] maxMusicVolume = new float[5] { 0.25f, 0.5f, 0.15f, 0.35f, 0.2f };

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        playerController = player.GetComponent<PlayerController>();
        currentStageNum = PlayerPrefs.GetInt("ClearStage", 0);
        musicNum = currentStageNum % 5;
        audioSource.volume = 0;
        audioSource.PlayOneShot(backGroundMusic[musicNum]);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (playerController.isPlaying)
        {
            audioSource.volume = 0;
        }
        else*/
        if(!playerController.isPlaying && playerController.timer <= 0)
        {
            if(audioSource.volume < maxMusicVolume[musicNum])
            {
                //audioSource.volume += 0.005f;
                audioSource.volume += maxMusicVolume[musicNum]/100;
            }
        }
    }

    
}
