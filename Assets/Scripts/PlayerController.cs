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
    public bool isZone;　//射撃ゾーンにいるかどうか
    public bool isPlaying; //プレイ中かどうか
    public float timer;
    bool canShot;
    int currentPlayerPosNum;
    float[] playerPos_y = new float[7] { -1.7f, -1.3f, -0.9f, -0.55f, -0.15f, 0.2f, 0.6f };
    public int playerLevel;
    int maxNoteNum;

    [SerializeField] GameObject timingCircle;
    CircleController circleController;
    [SerializeField] GameObject sceneManager;
    SceneScript sceneScript;

    [SerializeField] GameObject endLine;

    public AudioClip[] noteSound;
    AudioSource audioSource;
    public AudioClip damageSound;
    public AudioClip scoreSound;
    public AudioClip noteChangeSound;

    private Animator animator;

    int currentStageNum;
    float[] bpm = new float[15] { 60, 72, 80, 90, 105, 60, 72, 80, 90, 105, 60, 72, 80, 90, 105 };

    float[] shotTimingValues_Low = new float[5] { 0.05f, 0.05f, 0.075f, 0.075f, 0.075f };
    float[] shotTimingValues_High = new float[5] { 0.85f, 0.75f, 0.7f, 0.6f, 0.5f };
    float shotTimingValue_Low;
    float shotTimingValue_High;

    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.DeleteKey("SCORE");
        currentStageNum = PlayerPrefs.GetInt("ClearStage", 0);
        circleController = timingCircle.GetComponent<CircleController>();
        sceneScript = sceneManager.GetComponent<SceneScript>();
        audioSource = this.gameObject.GetComponent<AudioSource>();
        noteNum = 0;
        timer = 60;
        canShot = true;
        playerLevel = 1;
        currentPlayerPosNum = 3;
        isPlaying = false;
        isZone = true;
        playerLevel = 1;
        animator = this.GetComponent<Animator>();

        if(currentStageNum >= 5)
        {
            maxNoteNum = 1;
            if(currentStageNum >= 10)
            {
                maxNoteNum = 2;
            }
        }
        else
        {
            maxNoteNum = 0;
        }

        shotTimingValue_Low = shotTimingValues_Low[currentStageNum % 5];
        shotTimingValue_High = shotTimingValues_High[currentStageNum % 5];
        /*if(currentStageNum % 5 <= 2)
        {
            shotTimingValue_Low = 0.05f;
            shotTimingValue_High = 0.85f;
        }
        else
        {
            shotTimingValue_Low = 0.075f;
            shotTimingValue_High = 0.5f;
        }*/
    }

    // Update is called once per frame
    void Update()
    {
        if (isPlaying)
        {
            if (timer > 0)
            {
                animator.SetFloat("Horizontal", 0);
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
                        animator.SetFloat("Horizontal", 1.0f);
                        this.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
                    }
                }

                //左キーが押された時
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    if (this.transform.position.x >= -6.1f)
                    {
                        this.transform.position -= new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
                        animator.SetFloat("Horizontal", -1.0f);
                        this.transform.localScale = new Vector3(-0.75f, 0.75f, 0.75f);
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
                    if (circleController.timeCounter <= shotTimingValue_Low || circleController.timeCounter >= shotTimingValue_High)
                    {
                        if (Input.GetKeyDown(KeyCode.Space))
                        {
                            this.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
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
                                Invoke("Reload", 30 / bpm[currentStageNum]);
                            }
                        }
                    }
                }

                //音符の切り替え(Cキーが押された時)
                if (Input.GetKeyDown(KeyCode.C))
                {
                    int currentNoteNum = noteNum;
                    noteNum++;
                    if (noteNum > maxNoteNum)
                    {
                        noteNum = 0;
                    }
                    if(currentNoteNum != noteNum)
                    {
                        audioSource.PlayOneShot(noteChangeSound);
                    }
                }
            }
        }
        if (timer <= 0)
        {
            timer = 0;
            isPlaying = false;
            this.transform.localScale = new Vector3(0.75f, 0.75f, 0.75f);
            endLine.SetActive(false);
            animator.SetFloat("Horizontal", 1.0f);

            if (this.transform.position.x < 4.5f)
            {
                PlayerPrefs.SetInt("SCORE", score);
                PlayerPrefs.SetInt("ClearStage", currentStageNum);
                PlayerPrefs.Save();
                GameObject[] leftObjects;
                leftObjects = GameObject.FindGameObjectsWithTag("Eighth");
                foreach (GameObject note in leftObjects)
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

            if (this.transform.position.x <= 10)
            {
                this.transform.position += new Vector3(1.0f * moveSpeed, 0, 0) * Time.deltaTime;
            }
            else
            {
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
                if(playerLevel < 3)
                {
                    playerLevel++;
                }
                else
                {
                    AddScore(200);
                }
            }
            else
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
        audioSource.PlayOneShot(scoreSound);
        score += addScore;
    }

    public void Damage()
    {
        audioSource.PlayOneShot(damageSound);
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

    public void PlayStart()
    {
        isPlaying = true;
    }
}
