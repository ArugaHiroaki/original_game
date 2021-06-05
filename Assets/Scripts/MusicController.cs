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

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        playerController = player.GetComponent<PlayerController>();
        currentStageNum = PlayerPrefs.GetInt("ClearStage", 0);
        audioSource.PlayOneShot(backGroundMusic[currentStageNum]);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isPlaying)
        {
            audioSource.volume = 0;
        }
        else
        {
            if(audioSource.volume < 1)
            {
                audioSource.volume += 0.01f;
            }
        }
    }

    
}
