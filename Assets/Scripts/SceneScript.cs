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
    int lastStageNum = 14;

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
        /*if (currentSceneName == "GameOver")
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //LoadMain();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                //LoadTitle();
            }
        }*/
        if(currentSceneName == "GameClear")
        {

            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
            {
                if (clearStageNum < lastStageNum)
                {
                    //SceneManager.LoadScene("Stage2");
                    PlayerPrefs.SetInt("ClearStage", clearStageNum + 1);
                    PlayerPrefs.Save();
                }
                //LoadMain();
            }
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (clearStageNum < lastStageNum)
                {
                    PlayerPrefs.SetInt("ClearStage", clearStageNum + 1);
                    PlayerPrefs.Save();
                }
                //LoadTitle();
            }
        }
        if(currentSceneName == "Title")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                PlayerPrefs.DeleteKey("ClearStage");
                //LoadMain();
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //LoadMain();
            }
        }
    }

    public void LoadClear()
    {
        SceneManager.LoadScene("GameClear");
    }

    public void LoadMain()
    {
        SceneManager.LoadScene("Main");
    }

    public void LoadTitle()
    {
        SceneManager.LoadScene("Title");
    }
}
