using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameObject player;
    private PlayerController playerControllerScript;
    private float speed = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            Vector3 lookDirection = (transform.position - player.transform.position).normalized;

            transform.Translate(lookDirection * speed * Time.deltaTime);
        }
    }
}
