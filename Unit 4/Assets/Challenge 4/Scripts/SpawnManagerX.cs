using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject player; 
    public GameObject enemyPrefab;
    public GameObject powerupPrefab;


    private float spawnRangeX = 10;
    private float spawnZMin = 15; // set min spawn Z
    private float spawnZMax = 25; // set max spawn Z

    public float speedUpgrade = 0;
    public int enemyCount;
    public int waveCount = 1;

    private int powerUpCount;

    // Update is called once per frame
    void Update()
    {
        enemyCount = GameObject.FindGameObjectsWithTag("Enemy").Length;
        powerUpCount = GameObject.FindGameObjectsWithTag("Powerup").Length;

        if (enemyCount == 0)
        {
            SpawnEnemyWave(waveCount);
            ResetPlayerPosition();
        }

        if (powerUpCount == 0)
        {
            SpawnPowerUp();
        }
    }

    // Generate random spawn position for powerups and enemy balls
    Vector3 GenerateSpawnPosition ()
    {
        float xPos = Random.Range(-spawnRangeX, spawnRangeX);
        float zPos = Random.Range(spawnZMin, spawnZMax);
        return new Vector3(xPos, 0, zPos);
    }

    void SpawnEnemyWave(int enemiesToSpawn)
    {
        // Spawn number of enemy balls based on wave number
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }

        waveCount++;
        speedUpgrade += 5;
    }

    void SpawnPowerUp()
    {
        Vector3 powerupSpawnOffset = new Vector3(0, 0, -15); // make powerups spawn at player end

        Instantiate(powerupPrefab, GenerateSpawnPosition() + powerupSpawnOffset, powerupPrefab.transform.rotation);
    }

    // Move player back to position in front of own goal
    void ResetPlayerPosition()
    {
        player.transform.position = new Vector3(0, 1, -7);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero;
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

}
