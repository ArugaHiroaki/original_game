using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleUIController : MonoBehaviour
{

    [SerializeField] Text newGameText;
    [SerializeField] Text continueText;
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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine("Continue");
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine("NewGame");
        }
    }

    IEnumerator NewGame()
    {
        audioSource.PlayOneShot(selectSound);
        string tmpText = newGameText.text;
        newGameText.text = "";
        yield return new WaitForSeconds(0.25f);

        newGameText.text = tmpText;
        yield return new WaitForSeconds(0.25f);

        newGameText.text = "";
        yield return new WaitForSeconds(0.25f);

        newGameText.text = tmpText;
        yield return new WaitForSeconds(0.15f);

        sceneScript.LoadMain();
    }

    IEnumerator Continue()
    {
        audioSource.PlayOneShot(selectSound);
        string tmpText = continueText.text;
        continueText.text = "";
        yield return new WaitForSeconds(0.25f);

        continueText.text = tmpText;
        yield return new WaitForSeconds(0.25f);

        continueText.text = "";
        yield return new WaitForSeconds(0.25f);

        continueText.text = tmpText;
        yield return new WaitForSeconds(0.15f);

        sceneScript.LoadMain();
    }
}
