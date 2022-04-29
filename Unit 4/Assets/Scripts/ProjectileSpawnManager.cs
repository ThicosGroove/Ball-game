using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawnManager : MonoBehaviour
{
    public GameObject player;
    public GameObject projectilePrefab;

    float delay = 3f;

    void Update()
    {
        TriggerDelay();
    }

    void TriggerDelay()
    {
        delay -= Time.deltaTime;

        if (delay < 0 && Input.GetKeyDown("space"))
        {
                SpawnProjetcile();           
        }
    }

    void SpawnProjetcile()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy"); 

        for (int i = 0; i < enemies.Length; i++)
        {
            GameObject enemyTarget = 
                Instantiate(projectilePrefab,
                player.transform.position, 
                projectilePrefab.transform.rotation);
            enemyTarget.GetComponent<RocketManager>().SetTarget(enemies[i]);
        }

        delay = 3f;
    }
}
