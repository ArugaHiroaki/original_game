using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour
{

    [SerializeField] Text retryText;
    [SerializeField] Text forTitleText;
    [SerializeField] GameObject sceneManager;
    SceneScript sceneScript;
    [SerializeField] AudioClip selectSound;
    AudioSource audioSource;


    // Start is called before the first frame update
    void Start()
    {
        sceneScript = sceneManager.GetComponent<SceneScript>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift))
        {
            StartCoroutine("MoveMain");
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine("MoveTitle");
        }
    }

    IEnumerator MoveTitle()
    {
        audioSource.PlayOneShot(selectSound);
        string tmpText = forTitleText.text;
        forTitleText.text = "";
        yield return new WaitForSeconds(0.25f);

        forTitleText.text = tmpText;
        yield return new WaitForSeconds(0.25f);

        forTitleText.text = "";
        yield return new WaitForSeconds(0.25f);

        forTitleText.text = tmpText;
        yield return new WaitForSeconds(0.25f);

        sceneScript.LoadTitle();
    }

    IEnumerator MoveMain()
    {
        audioSource.PlayOneShot(selectSound);
        string tmpText = retryText.text;
        retryText.text = "";
        yield return new WaitForSeconds(0.25f);

        retryText.text = tmpText;
        yield return new WaitForSeconds(0.25f);

        retryText.text = "";
        yield return new WaitForSeconds(0.25f);

        retryText.text = tmpText;
        yield return new WaitForSeconds(0.2f);

        sceneScript.LoadMain();
    }
}
