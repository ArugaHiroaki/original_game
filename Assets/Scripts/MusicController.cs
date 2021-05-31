using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{

    [SerializeField] AudioClip backGroundMusic;
    AudioSource audioSource;

    [SerializeField] GameObject player;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        playerController = player.GetComponent<PlayerController>();
        audioSource.PlayOneShot(backGroundMusic);
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
