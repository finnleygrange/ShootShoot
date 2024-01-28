using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public GameObject projectilePrefab;
    public GameObject turret;
    public GameObject gameOverTxt;
    public Text ammoTxt;
    public Text LivesTxt;
    public bool gameOver;
    public int health = 3;
    public int ammo = 8;
    public AudioClip shootSound;
    public AudioClip emptyMagSound;
    public AudioClip ammoPickupSound;
    public AudioClip looseLifeSound;
    public AudioClip playerDiedSound;
    public AudioClip lifePickupSound;
    public AudioClip explosionSound;
    private AudioSource playerAudio;
    private float speed = 3.0f;
    private float xBound = 13.0f;
    private float zBound = 7.0f;

    // Start is called before the first frame update
    void Start()
    {
        playerAudio = GetComponent<AudioSource>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Shoot();
        StayInBounds();
        UpdateText();

    }

    private void UpdateText()
    {
        ammoTxt.GetComponent<UnityEngine.UI.Text>().text = ammo.ToString() + " / 8";
        LivesTxt.GetComponent<UnityEngine.UI.Text>().text = "Lives: " + health.ToString();
    }

    private void StayInBounds()
    {
        if (transform.position.x > xBound)
        {
            transform.position = new Vector3(xBound, transform.position.y, transform.position.z);
        } else if (transform.position.x < -xBound)
        {
            transform.position = new Vector3(-xBound, transform.position.y, transform.position.z);
        }
        if (transform.position.z > zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zBound);

        } else if (transform.position.z < -zBound)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBound);

        }
    }

    private void Move()
    {
        // If game is not over, allow the player to move
        if (!gameOver)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");

            transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
            transform.Translate(Vector3.forward * verticalInput * speed * Time.deltaTime);
        }
    }

    private void Shoot()
    {
        // If game not over and ammo is not 0 allow the player to shoot
        if (Input.GetKeyDown(KeyCode.Mouse0) && ammo != 0 && !gameOver)
        {
            Instantiate(projectilePrefab, turret.transform.position, turret.transform.rotation);
            ammo--;
            playerAudio.PlayOneShot(shootSound, 1);
        } else if (Input.GetKeyDown(KeyCode.Mouse0) && ammo == 0 && !gameOver)
        {
            playerAudio.PlayOneShot(emptyMagSound, 1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When player collides with enemy, take away health and destroy the enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            health--;
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(looseLifeSound, 1);

            if (health == 0)
            {
                playerAudio.PlayOneShot(playerDiedSound, 1);
                gameOver = true;
                gameOverTxt.gameObject.SetActive(true);
                StartCoroutine(EndGame());
            }
        }

        // When player collides with ammo, destroy ammo and give player more ammo
        if (other.gameObject.CompareTag("Ammo"))
        {
            ammo = 8;
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(ammoPickupSound, 1);
        }

        // When player collides with life, destroy life and add a life to the player 
        if (other.gameObject.CompareTag("Life"))
        {
            health++;
            Destroy(other.gameObject);
            playerAudio.PlayOneShot(lifePickupSound, 1);
        }
    }

  IEnumerator EndGame()
    {
        yield return new WaitForSeconds(5);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;
    }
}
