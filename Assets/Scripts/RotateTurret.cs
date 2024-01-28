using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateTurret : MonoBehaviour
{
    private float turnAmount = 90.0f;
    private GameObject player;
    private PlayerController playerControllerScript;


    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the turret around player by 90 degrees
        if (Input.GetKeyDown(KeyCode.E) && !playerControllerScript.gameOver)
        {
            transform.Rotate(Vector3.up * turnAmount);
        } else if (Input.GetKeyDown(KeyCode.Q) && !playerControllerScript.gameOver)
        {
            transform.Rotate(Vector3.up * -turnAmount);
        }
    }
}
