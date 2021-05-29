using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreateScript : MonoBehaviour
{

    public GameObject[] enemies;
    //float timeCounter;
    int enemyNum;
    float[] createPosition_y = new float[7];


    public TextAsset textAsset;

    // Start is called before the first frame update
    void Start()
    {
        enemyNum = enemies.Length;
        //timeCounter = 0.0f;
        for(int i = 0; i < 7; i++)
        {
            createPosition_y[i] = -1.725f + 0.4f * i;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(timeCounter >= 5.0f)
        {
            int enemykind = Random.Range(0, enemyNum);
            int position = Random.Range(0, 7);
            Instantiate(enemies[enemykind], new Vector3(this.transform.position.x, createPosition_y[position], this.transform.position.z), Quaternion.identity);
            timeCounter = 0.0f;
        }
        timeCounter += Time.deltaTime;
        */
    }

    void CreateEnemy()
    {
        int enemykind = Random.Range(0, enemyNum - 1);
        int position = Random.Range(0, 7);
        Instantiate(enemies[enemykind], new Vector3(this.transform.position.x, createPosition_y[position], this.transform.position.z), Quaternion.identity);
    }

    /*void LoadChart()
    {
        string jsonText = textAsset.ToString();
        JsonNode json = JsonNode.Parse(jsonText);
    }
    */
}
