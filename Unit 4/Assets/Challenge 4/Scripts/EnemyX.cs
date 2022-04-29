using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyX : MonoBehaviour
{
    public GameObject playerGoal;

    public float enemySpeed = 20;
    private Rigidbody enemyRb;
    private SpawnManagerX spawnManagerX;

    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        spawnManagerX = FindObjectOfType<SpawnManagerX>().GetComponent<SpawnManagerX>();
        enemySpeed += spawnManagerX.speedUpgrade;
    }

    // Update is called once per frame
    void Update()
    {
        // Set enemy direction towards player goal and move there
        Vector3 lookDirection = (playerGoal.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * enemySpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        // If enemy collides with either goal, destroy it
        if (other.gameObject.name == "Enemy Goal")
        {
            Destroy(gameObject);
        } 
        else if (other.gameObject.name == "Player Goal")
        {
            Destroy(gameObject);
        }
    }
}
