using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    float moveSpeed = 2f;　//左右の動きの速さ
    [SerializeField] GameObject[] notes;　//音符格納配列
    public int score;　//スコア
    public int playerHp = 10;　//現在のHP
    public int maxPlayerHp = 10;　//最大HP
    int noteNum;　//音符の種類の番号
    bool isZone;　//射撃ゾーンにいるかどうか
    public bool isPlaying; //プレイ中かどうか
    public float timer;
    bool canShot;
    int currentPlayerPosNum;
    float[] playerPos_y = new float[7] { -1.7f, -1.3f, -0.9f, -0.55f, -0.15f, 0.2f, 0.6f } ;
    private int playerLevel;

    [SerializeField] GameObject timingCircle;
    CircleController circleController;
    [SerializeField] GameObject sceneManager;
    SceneScript sceneScript;

    [SerializeField] GameObject endLine;

    public AudioClip[] noteSound;
    AudioSource audioSource;

    public float bpm;


    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey("SCORE");
        noteNum = 0;
        isPlaying = true;
        timer = 60;
        circleController = timingCircle.GetComponent<CircleController>();
        sceneScript = sceneManager.GetComponent<SceneScript>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
        canShot = true;
        playerLevel = 1;
        currentPlayerPosNum = 3;
        //score = 0;
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
                    if (currentPlayerPosNum <= 5)
                    {
                        currentPlayerPosNum++;
                        this.transform.position = new Vector2(this.transform.position.x, playerPos_y[currentPlayerPosNum]);
                    }
                }

                //下キーが押された時
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    if (currentPlayerPosNum >= 1)
                    {
                        currentPlayerPosNum--;
                        this.transform.position = new Vector2(this.transform.position.x, playerPos_y[currentPlayerPosNum]);
                    }
                }

                //音符の発射
                if (circleController.timeCounter <= 0.05f || circleController.timeCounter >= 0.95f)
                {
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        if (canShot)
                        {
                            Shot(currentPlayerPosNum);
                            if (playerLevel >= 2 && currentPlayerPosNum <= 4)
                            {
                                Shot(currentPlayerPosNum + 2);
                            }
                            if (playerLevel >= 3 && currentPlayerPosNum >= 2)
                            {
                                Shot(currentPlayerPosNum - 2);
                            }
                            Invoke("Reload", 30 / bpm);
                        }
                    }
                }
            }

            //音符の切り替え
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
            endLine.SetActive(false);
            if (this.transform.position.x < 4.5f)
            {
                PlayerPrefs.SetInt("SCORE", score);
                PlayerPrefs.Save();
                GameObject[] leftObjects;
                leftObjects = GameObject.FindGameObjectsWithTag("Eighth");
                foreach(GameObject note in leftObjects)
                {
                    Destroy(note.gameObject);
                }
                leftObjects = GameObject.FindGameObjectsWithTag("Quarter");
                foreach (GameObject note in leftObjects)
                {
                    Destroy(note.gameObject);
                }
                leftObjects = GameObject.FindGameObjectsWithTag("Friend");
                foreach (GameObject note in leftObjects)
                {
                    Destroy(note.gameObject);
                }
            }

            if(this.transform.position.x <= 10)
            {
                this.transform.position += new Vector3(1.5f * moveSpeed, 0, 0) * Time.deltaTime;
            }
            else
            {
                //sceneManager.SendMessage("LoadClear");
                sceneScript.LoadClear();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Friend")
        {
            if(collision.gameObject.transform.position.x > -0.5f)
            {
                AddScore(200);
                playerLevel++;
            } else
            {
                AddScore(100);
            }
        }
        else
        {
            Damage();
        }
        Destroy(collision.gameObject);
    }

    public void AddScore(int addScore)
    {
        score += addScore;
    }

    public void Damage()
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

    void Shot(int posNum)
    {
        Instantiate(notes[noteNum], new Vector2(this.transform.position.x + 1, playerPos_y[posNum]), Quaternion.identity);
        audioSource.PlayOneShot(noteSound[posNum]);
        canShot = false;
    }

    void Reload()
    {
        canShot = true;
    }
}
