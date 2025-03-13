using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform companion; //Reference to the companion
    public float followSpeed; //Speed of following
    public float followDistance; //Distance threshold for following


    void Update()
    {
        //Calculate distance between Enemy and Companion
        float distance = Vector3.Distance(transform.position, companion.position);

        //If the distance is greater than the follow distance, move toward the player
        if (distance > followDistance)
        {
            Vector3 targetPosition = companion.position;
            targetPosition.z = transform.position.z; //Optional,if 2D
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);

        }
    }

    public int maxHealth;
    private int currentHealth;
    void Start()
    {
        currentHealth = maxHealth;
        
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            TakeDamage();
            Destroy(collision.gameObject);
            Debug.Log("Hit");
        }

    }
    void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            Destroyed();
        }
    }
    void Destroyed()
    {
        Destroy(gameObject);
    }

}