using System.Reflection;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
    public Transform companion; //Reference to the companion
    public float followSpeed; //Speed of following
    public float followDistance; //Distance threshold for following

    public GameObject hpBarGreen;
    public GameObject hpBarRed;
    public Vector3 hpBarOffset = new Vector3(0f,1f,0f);

    public int maxHealth;
    private int currentHealth;
    public GameObject coinPrefab;
    void Start()
    {
        companion = GameObject.FindGameObjectWithTag("Companion").transform;
        if(companion == null)
        {
            Debug.LogError("Companion not found! Make sure your companion has the correct tag.");
        }
        currentHealth = maxHealth;
        if (hpBarGreen != null && hpBarRed != null)
        {
            hpBarGreen.SetActive(false);
            hpBarRed.SetActive(false);
        }
    }
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
        transform.position = Vector3.MoveTowards(transform.position, companion.position, followSpeed * Time.deltaTime); 
        if (hpBarGreen != null && hpBarRed != null)
        {
            hpBarGreen.transform.position = transform.position + hpBarOffset;
            hpBarRed.transform.position = transform.position + hpBarOffset;
        }
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

        if (hpBarGreen != null && hpBarRed != null)
        {
            hpBarGreen.SetActive(true);
            hpBarRed.SetActive(true);
            UpdateHpBar();

            if (currentHealth <= 0)
            {
                Destroyed();
            }
        }
    }

    private void UpdateHpBar()
    {
        float healthPercentage = (float)currentHealth / maxHealth;
        Vector3 scale = hpBarGreen.transform.localScale;
        scale.x = healthPercentage;
        hpBarGreen.transform.localScale = scale;
    }

    void Destroyed()
    {
        UIManager.Instance.KillCount();
        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }

}