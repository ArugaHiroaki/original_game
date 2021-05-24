using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreateScript : MonoBehaviour
{

    public GameObject[] enemies;
    float timeCounter;
    [SerializeField] int enemyNum;

    // Start is called before the first frame update
    void Start()
    {
        timeCounter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(timeCounter >= 5.0f)
        {
            int enemykind = Random.Range(0, enemyNum);
            Instantiate(enemies[enemykind], this.transform.position, Quaternion.identity);
            timeCounter = 0.0f;
        }
        timeCounter += Time.deltaTime;
    }
}
