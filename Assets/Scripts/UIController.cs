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
    [SerializeField] Text countDownText;
    [SerializeField] Text playerLevelText;
    [SerializeField] AudioClip countDownSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        audioSource = GetComponent<AudioSource>();
        slider.value = 1;
        stageText.text = "Stage" + (PlayerPrefs.GetInt("ClearStage", 0) + 1);
        countDownText.text = "";
        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        float sliderValue = (float)playerController.playerHp / (float)playerController.maxPlayerHp;
        scoreText.text = "Score: " + playerController.score;
        playerHpText.text = "HP: " + playerController.playerHp;
        timeText.text = "Time: " + playerController.timer.ToString("f1");
        slider.value = sliderValue;
        if(playerController.playerLevel == 3)
        {
            playerLevelText.text = "PlayerLevel: MAX";
        }
        else
        {
            playerLevelText.text = "PlayerLevel: " + playerController.playerLevel;
        }
    }

    IEnumerator CountDown()
    {
        countDownText.text = "3";
        audioSource.PlayOneShot(countDownSound);
        yield return new WaitForSeconds(1.0f);

        countDownText.text = "2";
        audioSource.PlayOneShot(countDownSound);
        yield return new WaitForSeconds(1.0f);

        countDownText.text = "1";
        audioSource.PlayOneShot(countDownSound);
        yield return new WaitForSeconds(1.0f);

        countDownText.text = "START";
        yield return new WaitForSeconds(1.0f);

        countDownText.text = "";
        playerController.PlayStart();
    }
}
