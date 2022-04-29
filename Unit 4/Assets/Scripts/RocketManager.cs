using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketManager : MonoBehaviour
{
    Rigidbody rocketRb;
    Collider rocketCollider, player;
    Transform enemy;
    Vector3 target;

    [SerializeField] float rocketSpeed = 5f;
    [SerializeField] float rocketStrength = 15f;


    // Start is called before the first frame update
    void Start()
    {
        rocketRb = GetComponent<Rigidbody>();

        rocketCollider = GetComponent<Collider>();
        player = GameObject.Find("Player").GetComponent<Collider>();

        Physics.IgnoreCollision(player, rocketCollider, true);

        StartCoroutine(DestroyProjectile());
    }

    // Update is called once per frame
    void Update()
    {       
        FollowEnemy();
    }

    void FollowEnemy()
    {
        if (enemy != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, rocketSpeed * Time.deltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            enemyRB.AddForce(awayFromPlayer * rocketStrength, ForceMode.Impulse);

            Destroy(this.gameObject);
        }
    }

    public void SetTarget(GameObject enemyTarget)
    {
        enemy = enemyTarget.transform;
        target = new Vector3(enemy.position.x, 0, enemy.position.z);
    }

    IEnumerator DestroyProjectile()
    {
        yield return new WaitForSeconds(0.5f);

        if (rocketRb.velocity == Vector3.zero)
        {
            Destroy(this.gameObject);
        }
        else yield return new WaitForSeconds(2f);
        Destroy(this.gameObject);
    }
}

