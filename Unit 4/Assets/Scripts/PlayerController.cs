using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private PowerUpType powerUpType;

    public GameObject powerUpIndicator;
    public GameObject ballonPUIndicator;

    Rigidbody rb;

    public float speed = 15;
    public float powerUpStrength = 15f;
    public bool hasPowerUp = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();      
    }

    // Update is called once per frame
    void Update()
    {
        float fowardInput = Input.GetAxis("Vertical");
        float sideInput = Input.GetAxis("Horizontal");

        //rb.AddForce(focalPoint.transform.forward * fowardInput * speed);   

        rb.AddForce(new Vector3(sideInput, 0, fowardInput) * speed);

        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.7f, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Power Up"))
        {
            powerUpType = 0;
            hasPowerUp = true;
            powerUpIndicator.SetActive(true);
            Debug.Log("Power UP!!!");
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Power Up"))
        {
            hasPowerUp = true;
            ballonPUIndicator.SetActive(true);
            Debug.Log("Power UP!!!");
            Destroy(other.gameObject);
        }


        StartCoroutine(PowerUpCountdownRoutine());
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRB = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collided with " + collision.gameObject.name + " whith powerup set to " + hasPowerUp);

            enemyRB.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
        }
    }

    IEnumerator PowerUpCountdownRoutine()
    {
        yield return new WaitForSeconds(7);

        hasPowerUp = false;
        powerUpIndicator.SetActive(false);
        ballonPUIndicator.SetActive(false);
        Debug.Log("Power Up Has Endded");
    }
}
