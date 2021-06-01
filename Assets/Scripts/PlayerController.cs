using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float moveSpeed = 2.5f;　//左右の動きの速さ
    [SerializeField] GameObject[] notes;　//音符格納配列
    public int score;　//スコア
    public int playerHp = 10;　//現在のHP
    public int maxPlayerHp = 10;　//最大HP
    int noteNum;　//音符の種類の番号
    bool isZone;　//射撃ゾーンにいるかどうか
    public bool isPlaying; //プレイ中かどうか
    public float timer;
    bool canShot;

    [SerializeField] GameObject timingCircle;
    CircleController circleController;
    [SerializeField] GameObject sceneManager;
    SceneScript sceneScript;


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey("SCORE");
        noteNum = 0;
        isPlaying = true;
        timer = 60;
        circleController = timingCircle.GetComponent<CircleController>();
        sceneScript = sceneManager.GetComponent<SceneScript>();
        canShot = true;
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0)
        {
            timer = 0;
            isPlaying = false;
        }
        if (isPlaying)
        {
            timer -= Time.deltaTime;
            if (this.gameObject.transform.position.x <= -5)
            {
                isZone = true;
            }
            else
            {
                isZone = false;
            }

            //右キーが押された時
            if (Input.GetKey(KeyCode.RightArrow))
            {
                if (this.transform.position.x <= 4)
                {
                    this.transform.position += new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
                }
            }

            //左キーが押された時
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                if (this.transform.position.x >= -6.1f)
                {
                    this.transform.position -= new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
                }
            }

            if (isZone)
            {

                //上キーが押された時
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    if (this.transform.position.y < 0.6f)
                    {
                        this.transform.position += new Vector3(0, 0.4f, 0);
                    }
                }

                //下キーが押された時
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (this.transform.position.y >= -1.7f)
                    {
                        this.transform.position -= new Vector3(0, 0.4f, 0);
                    }
                }

                if (circleController.timeCounter <= 0.05f || circleController.timeCounter >= 0.95f)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        if (canShot)
                        {
                            Instantiate(notes[noteNum], this.transform.position + new Vector3(1, 0, 0), Quaternion.identity);
                            canShot = false;
                            Invoke("Reload", 0.5f);
                        }
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
                noteNum++;
                if (noteNum >= 2)
                {
                    noteNum = 0;
                }
            }
        }
        else
        {
            if (this.transform.position.x < 4.5f)
            {
                PlayerPrefs.SetInt("SCORE", score);
                PlayerPrefs.Save();
                GameObject[] eighth;
                GameObject[] quarter;
                eighth = GameObject.FindGameObjectsWithTag("Eighth");
                quarter = GameObject.FindGameObjectsWithTag("Quarter");
                foreach(GameObject note in eighth)
                {
                    Destroy(note.gameObject);
                }
                foreach(GameObject note in quarter)
                {
                    Destroy(note.gameObject);
                }
            }

            if(this.transform.position.x < 9)
            {
                this.transform.position += new Vector3(1.5f * moveSpeed, 0, 0) * Time.deltaTime;
            }
            else
            {
                sceneManager.SendMessage("LoadClear");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Friend")
        {

        }
        else
        {
            Damage();
            Destroy(collision.gameObject);
        }
    }

    void AddScore(int addScore)
    {
        score += addScore;
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

    void Reload()
    {
        canShot = true;
    }
}
