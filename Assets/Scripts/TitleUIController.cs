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


    // Start is called before the first frame update
    void Start()
    {
        sceneScript = sceneManager.GetComponent<SceneScript>();
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
        string tmpText = newGameText.text;
        newGameText.text = "";
        yield return new WaitForSeconds(0.25f);

        newGameText.text = tmpText;
        yield return new WaitForSeconds(0.25f);

        newGameText.text = "";
        yield return new WaitForSeconds(0.25f);

        newGameText.text = tmpText;
        yield return new WaitForSeconds(0.25f);

        sceneScript.LoadMain();
    }

    IEnumerator Continue()
    {
        string tmpText = continueText.text;
        continueText.text = "";
        yield return new WaitForSeconds(0.25f);

        continueText.text = tmpText;
        yield return new WaitForSeconds(0.25f);

        continueText.text = "";
        yield return new WaitForSeconds(0.25f);

        continueText.text = tmpText;
        yield return new WaitForSeconds(0.25f);

        sceneScript.LoadMain();
    }
}
