using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float moveSpeed = 5.0f;
    [SerializeField] GameObject[] notes;
    public int score;
    public int playerHp = 10;
    public int maxPlayerHp = 10;

    int noteNum;

    // Start is called before the first frame update
    void Start()
    {
        noteNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position -= new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (this.transform.position.y < 0.6f)
            {
                this.transform.position += new Vector3(0, 0.4f, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (this.transform.position.y >= -1.7f)
            {
                this.transform.position -= new Vector3(0, 0.4f, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(notes[noteNum], this.transform.position + new Vector3(1, 0, 0), Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.C))
        {
            noteNum++;
            if(noteNum >= 2)
            {
                noteNum = 0;
            }
        }
    }

    void AddScore()
    {
        score += 100;
    }

    void Damage()
    {
        playerHp--;
        if (score >= 100)
        {
            score -= 100;
        }
    }

    void Recover()
    {
        if(playerHp < 10)
        {
            playerHp++;
        }
    }
}
