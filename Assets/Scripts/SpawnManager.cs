using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRangeZ = 7.5f;
    private float spawnRangeX = 13.0f;
    private float ammoStartDelay = 5.0f;
    private float ammoRepeatRate = 10.0f;
    private float lifeStartDelay = 10.0f;
    private float lifeRepeatRate = 20.0f;
    private GameObject player;
    private PlayerController playerControllerScript;

    public GameObject enemyPrefab;
    public GameObject ammoPrefab;
    public GameObject lifePrefab;
    public int enemyCount;
    public int waveNumber = 1;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        playerControllerScript = player.GetComponent<PlayerController>();

        SpawnEnemyWave(waveNumber);
        InvokeRepeating("SpawnAmmo", ammoStartDelay, ammoRepeatRate);
        InvokeRepeating("SpawnLife", lifeStartDelay, lifeRepeatRate);
    }

    // Update is called once per frame
    void Update()
    {
        // When there are no enemyies, increase the wave number and spawn enemies
        enemyCount = FindObjectsOfType<Enemy>().Length;

        if (enemyCount == 0)
        {
            waveNumber++;
            SpawnEnemyWave(waveNumber);
        }
    }

    private void SpawnAmmo()
    {
        if (!playerControllerScript.gameOver)
        {
            // Spawn ammo
            Instantiate(ammoPrefab, GenerateRandomPos(), enemyPrefab.transform.rotation);
        }
    }

    private void SpawnLife()
    {
        if (!playerControllerScript.gameOver)
        {
            // Spawn Life
            Instantiate(lifePrefab, GenerateRandomPos(), lifePrefab.transform.rotation);
        }
    }

    private void SpawnEnemyWave(int waveNumber)
    {
        // Spawn an extra enemy each wave
        for (int i = 0; i < waveNumber; i++)
        {
            Instantiate(enemyPrefab, GenerateRandomPos(), enemyPrefab.transform.rotation);
        }
    }

    private Vector3 GenerateRandomPos()
    {
        // Generate random position on the screen
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);

        Vector3 randomPos = new Vector3(spawnPosX, enemyPrefab.transform.position.y, spawnPosZ);
        return randomPos;
    }
}
