using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class EnemyInfo
{
    public string name;
    public GameObject prefab;
}

public class SpanwManager : MonoBehaviour
{
    public EnemyInfo[] enemies;
    

    //public GameObject enemyPrefab;
    public GameObject powerUpPrefab;

    public int waveNumer = 1;

    private float spawnRange = 9f;
    private int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        SpawnWave(waveNumer);
        SpawnPowerUp();
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumer++;
            SpawnWave(waveNumer);
            SpawnPowerUp();
        }

    }

    void SpawnWave(int enemiesToSpanw)
    {
        for (int i = 0; i < enemiesToSpanw; i++)
        {
            int enemy = Random.Range(0, enemies.Length);

            Instantiate(enemies[enemy].prefab, GenerateSpawnPosition(), enemies[enemy].prefab.transform.rotation);
        }
    }

    void SpawnPowerUp()
    {
        Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
    }

    Vector3 GenerateSpawnPosition()
    {
        float spawnPosX = Random.Range(0, spawnRange);
        float spawnPosZ = Random.Range(spawnRange, 0);
        Vector3 randomPos = new Vector3(spawnPosX, 0, spawnPosZ);

        return randomPos;
    }

    void RandomizeEnemy()
    {
        int enemy = Random.Range(0, enemies.Length);
    }
}
