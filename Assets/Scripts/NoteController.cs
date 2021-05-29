using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoteController : MonoBehaviour
{

    float moveSpeed = 1.5f;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position += new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;
        if (transform.position.x >= 9)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(this.gameObject.tag == collision.gameObject.tag)
        {
            if(this.gameObject.transform.position.x >= -0.5f)
            {
                player.SendMessage("AddScore", 200);
            }
            else
            {
                player.SendMessage("AddScore", 100);
            }
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "Friend")
        {
            player.SendMessage("Damage");
            Destroy(collision.gameObject);
        }
        Destroy(this.gameObject);
    }
}
