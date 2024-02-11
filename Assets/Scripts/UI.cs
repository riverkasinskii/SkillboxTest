using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{    
    [SerializeField] private TextMeshProUGUI redGemsText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Slider healthBar;
       
    private void Awake()
    {
        SearchGameSessions();
    }
        
    private void Start()
    {
        healthBar.value = 1f;
    }
        
    public void UpdateRedGems()
    {
        redGemsText.text = Stats.RedGems.ToString("D3");
    }

    public void UpdateCoins()
    {
        coinsText.text = Stats.Coins.ToString("D3");
    }

    public void UpdateHealthBar(float damage)
    {
        healthBar.value -= damage / 100;        
    }

    public void ResetHealthBar()
    {
        healthBar.value = 1f;
    }

    private void SearchGameSessions()
    {
        int numGameSessions = FindObjectsOfType<UI>().Length;

        if (numGameSessions > 1)
        {
            gameObject.SetActive(false);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
}
