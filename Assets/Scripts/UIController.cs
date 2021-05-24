using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{

    [SerializeField] Text scoreText;
    [SerializeField] GameObject player;
    [SerializeField] Slider slider;
    [SerializeField] Text playerHpText;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        slider.value = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float sliderValue = (float)playerController.playerHp / (float)playerController.maxPlayerHp;
        scoreText.text = "Score: " + playerController.score;
        playerHpText.text = "HP: " + playerController.playerHp;
        slider.value = sliderValue;
    }
}
