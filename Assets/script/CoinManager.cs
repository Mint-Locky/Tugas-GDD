using UnityEngine;

public class CoinManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public int coinValue = 1;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        { UIManager.Instance.AddCoin(coinValue);
            Destroy(gameObject);
        }
    }
}
