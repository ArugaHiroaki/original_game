using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneScript : MonoBehaviour
{
    [SerializeField] GameObject player;
    PlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Main")
        {
            if(playerController.playerHp <= 0)
            {
                SceneManager.LoadScene("GameOver");
            }
            /*if(playerController.timer == 0)
            {
                SceneManager.LoadScene("GameClear");
            }*/
        }
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Main");
            }
        }
        if(SceneManager.GetActiveScene().name == "GameClear")
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("Main");
            }
        }
        if(SceneManager.GetActiveScene().name == "Title")
        {
            if (Input.GetKeyDown(KeyCode.Return))
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
