using UnityEngine;

public class CompanionFollow : MonoBehaviour
{
    public Transform player; //Reference to the Player
    public float followSpeed; //Speed of following
    public float followDistance; //Distance threshold for following


    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Companion"), LayerMask.NameToLayer("Player"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Companion"), LayerMask.NameToLayer("Bullet"));
        currentHealth = maxHealth;
        CompcurrentHealth = CompmaxHealth;
    }

    void Update()
    {
        //Calculate distance between Companion and Player
        float distance = Vector3.Distance(transform.position, player.position);

        //If the distance is greater than the follow distance, move toward the player
        if (distance > followDistance )
        {
            Vector3 targetPosition = player.position;
            targetPosition.z = transform.position.z; //Optional,if 2D
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
        
        }
    }

    public int maxHealth;
    private int currentHealth;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakeDamage();
            Debug.Log("Companion Hit");
        }
    }
    void TakeDamage()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            Destroyed();
        }
        //returns the higher of the two values
        CompcurrentHealth = Mathf.Max(CompcurrentHealth - 1, 0);
        UpdateHpBar(); //Update HP bar

        if (CompcurrentHealth <= 0)
        {  Destroyed(); }
        else { StartCoroutine(InvincibilitiyCooldown()); //mulai cooldown
    }

    IEnumerator InvincibilityCooldown()
        {
            isInvincible = true; //Companion kebal sementara
            yield return new WaitForSeconds(invincibilitiyDuration);
            isInvincible = false; //Bisa kena hit lagi
        }
    void Destroyed()
    {
        Destroy(gameObject);
    }

    private int CompcurrentHealth;
    public int CompmaxHealth;
    private bool isInvincible = false;
    public float invincibilitiyDuration = 1.5f;
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && isInvincible)
        {
            TakeDamage();
            Debug.Log("Companion Hit!");
        }
    }
    
}
