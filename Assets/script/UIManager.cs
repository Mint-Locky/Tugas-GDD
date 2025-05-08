using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI coinCountText;
    private int totalCoins = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static UIManager Instance;
    public Image companionHPBar;
    public TextMeshProUGUI killCountText;
    private int killCount = 0;
    public CanvasGroup GAMEOVER;
    public void ShowGameOver() 
    {
        Debug.Log("Test");
        GAMEOVER.alpha = 1.0f;
    }

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateCompanionHP(float currentHealth, float maxHealth)
    {
        if (companionHPBar != null)
        {
            companionHPBar.fillAmount = currentHealth / maxHealth;
        }
    }
    public void KillCount()
    {
        killCount++;
        UpdateKillCount();
    }
    private void UpdateKillCount()
    {
        if (killCountText !=null) 
        { killCountText.text = "Kills:" + killCount; }
    }
    public void AddCoin(int amount)
    { totalCoins += amount;
        UpdateCoinUI();
    }
    private void UpdateCoinUI()
    {
       if(coinCountText !=null)
        { coinCountText.text = "Coins: " + totalCoins; }
    }
}
