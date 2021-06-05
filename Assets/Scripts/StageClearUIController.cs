using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClearUIController : MonoBehaviour
{

    [SerializeField] Text scoreText;
    int score;
    [SerializeField] Text forNextLevelText;
    int clearStageNum;
    int lastStageNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("SCORE", 0);
        scoreText.text = "Score: " + score;
        clearStageNum = PlayerPrefs.GetInt("ClearStage", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(clearStageNum == lastStageNum)
        {
            forNextLevelText.text = "PRESS SPACE FOR RETRY";
        }
        /*int score = 0;
        if(score < gameScore)
        {
            score++;
            scoreText.text = "Score: " + score;
        }*/
    }
}
