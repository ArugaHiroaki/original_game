using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedScoreScript : MonoBehaviour
{

    public GameObject[] selectedScores;
    int scoreNum;
    int currentStageNum;
    int maxNoteNum;
    [SerializeField] GameObject player;
    PlayerController playerController;
    [SerializeField] GameObject cross;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        scoreNum = 0;
        selectedScores[0].SetActive(true);
        currentStageNum = PlayerPrefs.GetInt("ClearStage", 0);
        cross.SetActive(false);
        if (currentStageNum >= 5)
        {
            maxNoteNum = 1;
            if (currentStageNum >= 10)
            {
                maxNoteNum = 2;
            }
        }
        else
        {
            maxNoteNum = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (playerController.isPlaying)
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                selectedScores[scoreNum].SetActive(false);
                scoreNum++;
                if (scoreNum > maxNoteNum)
                {
                    scoreNum = 0;
                }
                selectedScores[scoreNum].SetActive(true);
            }
        }
        if (playerController.isZone)
        {
            cross.SetActive(false);
        }
        else
        {
            cross.SetActive(true);
        }
    }
}
