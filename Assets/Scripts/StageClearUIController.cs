using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StageClearUIController : MonoBehaviour
{

    [SerializeField] Text scoreText;
    int score;
    [SerializeField] Text forNextLevelText;
    [SerializeField] Text forTitleText;
    int clearStageNum;
    int lastStageNum = 14;
    SceneScript sceneScript;
    [SerializeField] GameObject sceneManager;
    [SerializeField] AudioClip selectSound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        score = PlayerPrefs.GetInt("SCORE", 0);
        scoreText.text = "Score: " + score;
        clearStageNum = PlayerPrefs.GetInt("ClearStage", 0);
        sceneScript = sceneManager.GetComponent<SceneScript>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if(clearStageNum == lastStageNum)
        {
            forNextLevelText.text = "PRESS SPACE FOR RETRY";
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("MoveMain");
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine("MoveTitle");
        }
        /*int score = 0;
        if(score < gameScore)
        {
            score++;
            scoreText.text = "Score: " + score;
        }*/
    }

    IEnumerator MoveTitle()
    {
        audioSource.PlayOneShot(selectSound);
        forTitleText.text = "";
        yield return new WaitForSeconds(0.25f);

        forTitleText.text = "PRESS RETURN TO TITLE";
        yield return new WaitForSeconds(0.25f);

        forTitleText.text = "";
        yield return new WaitForSeconds(0.25f);

        forTitleText.text = "PRESS RETURN TO TITLE";
        yield return new WaitForSeconds(0.25f);

        sceneScript.LoadTitle();
    }

    IEnumerator MoveMain()
    {
        audioSource.PlayOneShot(selectSound);
        string tmpText = forNextLevelText.text;
        forNextLevelText.text = "";
        yield return new WaitForSeconds(0.25f);

        forNextLevelText.text = tmpText;
        yield return new WaitForSeconds(0.25f);

        forNextLevelText.text = "";
        yield return new WaitForSeconds(0.25f);

        forNextLevelText.text = tmpText;
        yield return new WaitForSeconds(0.2f);

        sceneScript.LoadMain();
    }
}
