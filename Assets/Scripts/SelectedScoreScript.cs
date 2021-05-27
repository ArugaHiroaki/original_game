using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedScoreScript : MonoBehaviour
{

    public GameObject[] selectedScores;
    int scoreNum;

    // Start is called before the first frame update
    void Start()
    {
        scoreNum = 0;
        selectedScores[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            selectedScores[scoreNum].SetActive(false);
            scoreNum++;
            if(scoreNum >= selectedScores.Length)
            {
                scoreNum = 0;
            }
            selectedScores[scoreNum].SetActive(true);
        }
    }
}
