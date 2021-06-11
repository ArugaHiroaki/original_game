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
            StartCoroutine("MoveMain");
        }
        if (Input.GetKeyDown(KeyCode.Return))
        {
            StartCoroutine("MoveTitle");
        }
    }

    IEnumerator MoveTitle()
    {
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
        string tmpText = retryText.text;
        retryText.text = "";
        yield return new WaitForSeconds(0.25f);

        retryText.text = tmpText;
        yield return new WaitForSeconds(0.25f);

        retryText.text = "";
        yield return new WaitForSeconds(0.25f);

        retryText.text = tmpText;
        yield return new WaitForSeconds(0.25f);

        sceneScript.LoadMain();
    }
}
