using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleController : MonoBehaviour
{

    [SerializeField] private LineRenderer m_lineRenderer; //円を描画するためのLineRenderer
    [SerializeField] private float m_radius; //円の半径
    [SerializeField] private float m_lineWidth; //円の線の太さ

    [SerializeField] private float m_duration; //スケール演出の再生時間
    [SerializeField] private float m_from; //スケール演出の開始値
    [SerializeField] private float m_to; //スケール演出の終了値

    private float m_elapedTime;
    public float timeCounter;

    int beatCount;
    public int enemyCreateBeat;
    [SerializeField] GameObject enemyCreater;

    public AudioClip drumSound;
    AudioSource audioSource;

    private void Reset()
    {
        m_lineRenderer = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        InitLineRenderer();
        audioSource = GetComponent<AudioSource>();
        timeCounter = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        /*
        m_elapedTime += Time.deltaTime;
        var amount = m_elapedTime % m_duration / m_duration;
        var scale = Mathf.Lerp(m_from, m_to, amount);
        */
        m_elapedTime += Time.deltaTime;
        var amount = m_elapedTime % m_duration / m_duration;
        //Debug.Log(amount);
        /*if (amount >= 0.99f)
        {
            Debug.Log("Sound");
            audioSource.PlayOneShot(drumSound);
        }
        */
        var scale = Mathf.Lerp(m_from, m_to, amount);
        transform.localScale = new Vector3(scale, scale, 1);
        timeCounter += Time.deltaTime;
        if(timeCounter >= m_duration)
        {
            audioSource.PlayOneShot(drumSound);
            timeCounter -= m_duration;
            beatCount++;
            if(beatCount % enemyCreateBeat == 0)
            {
                beatCount = 0;
                enemyCreater.SendMessage("CreateEnemy");
            }
        }
    }

    void InitLineRenderer()
    {
        var segments = 360;

        m_lineRenderer.startWidth = m_lineWidth;
        m_lineRenderer.endWidth = m_lineWidth;
        m_lineRenderer.positionCount = segments;
        m_lineRenderer.loop = true;
        m_lineRenderer.useWorldSpace = false; //transform.localScaleを適用するため

        var points = new Vector3[segments];

        for (int i = 0; i < segments; i++)
        {
            var rad = Mathf.Deg2Rad * (i * 360f / segments);
            var x = Mathf.Sin(rad) * m_radius;
            var y = Mathf.Cos(rad) * m_radius;
            points[i] = new Vector3(x, y, 0);
        }

        m_lineRenderer.SetPositions(points);

    }
}
