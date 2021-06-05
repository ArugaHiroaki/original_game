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
    [SerializeField] Text timeText;
    [SerializeField] Text stageText;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        slider.value = 1;
        stageText.text = "Stage" + (PlayerPrefs.GetInt("ClearStage", 0) + 1);
    }

    // Update is called once per frame
    void Update()
    {
        float sliderValue = (float)playerController.playerHp / (float)playerController.maxPlayerHp;
        scoreText.text = "Score: " + playerController.score;
        playerHpText.text = "HP: " + playerController.playerHp;
        timeText.text = "Time: " + playerController.timer.ToString("f1");
        slider.value = sliderValue;
    }
}
