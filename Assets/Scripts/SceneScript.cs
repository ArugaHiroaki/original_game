using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    PlayerController playerController;

    string currentSceneName;

    int clearStageNum;
    int lastStageNum = 1;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        currentSceneName = SceneManager.GetActiveScene().name;
        clearStageNum = PlayerPrefs.GetInt("ClearStage", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSceneName == "Main")
        {
            if(playerController.playerHp <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
        }
        if (currentSceneName == "GameOver")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Main");
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Title");
            }
        }
        if(currentSceneName == "GameClear")
        {
            if(clearStageNum < lastStageNum)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    //SceneManager.LoadScene("Stage2");
                    PlayerPrefs.SetInt("ClearStage", clearStageNum + 1);
                    PlayerPrefs.Save();
                    SceneManager.LoadScene("Main");
                }
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (clearStageNum < lastStageNum)
                {
                    PlayerPrefs.SetInt("ClearStage", clearStageNum + 1);
                    PlayerPrefs.Save();
                }
                SceneManager.LoadScene("Title");
            }
        }
        if(currentSceneName == "Title")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                PlayerPrefs.DeleteKey("ClearStage");
                SceneManager.LoadScene("Main");
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene("Main");
            }
        }
    }

    public void LoadClear()
    {
        SceneManager.LoadScene("GameClear");
    }
}
