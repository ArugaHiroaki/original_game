using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestController : MonoBehaviour
{

    float moveSpeed = 3.0f;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= new Vector3(1, 0, 0) * moveSpeed * Time.deltaTime;

        if(transform.position.x <= -6.5f)
        {
            if(this.gameObject.tag == "Friend")
            {
                player.SendMessage("Recover");
            }
            else
            {
                player.SendMessage("Damage");
            }
            Destroy(this.gameObject);
        }
    }
}
