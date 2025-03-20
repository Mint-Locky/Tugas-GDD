using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompanionFollow : MonoBehaviour
{
    public Transform player; //Reference to the Player
    public float followSpeed; //Speed of following
    public float followDistance; //Distance threshold for following
    private int CompcurrentHealth;
    public int CompmaxHealth;
    private bool isInvincible = false;
    public float invincibilitiyDuration = 1.5f;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Companion"), LayerMask.NameToLayer("Player"));
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Companion"), LayerMask.NameToLayer("Bullet"));
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
        CompcurrentHealth = Mathf.Max(CompcurrentHealth - 1, 0);
        if (CompcurrentHealth <= 0)
        {
            Destroyed();
        }
        else { StartCoroutine(InvincibilityCooldown()); }
        //returns the higher of the two values
        CompcurrentHealth = Mathf.Max(CompcurrentHealth - 1, 0);


        if (CompcurrentHealth <= 0)
        { Destroyed(); }
        else
        {
            StartCoroutine(InvincibilityCooldown()); //mulai cooldown
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
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage();
            Debug.Log("Companion Hit!");
        }
    }
    
}
