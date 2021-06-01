using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClearUIController : MonoBehaviour
{

    [SerializeField] Text scoreText;
    int score;


    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("SCORE", 0);
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        /*int score = 0;
        if(score < gameScore)
        {
            score++;
            scoreText.text = "Score: " + score;
        }*/
    }
}
