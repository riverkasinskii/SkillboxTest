using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Collections.Generic;

public class UI : MonoBehaviour, ISaveable
{
    private const string RED_GEMS_KEY = "redGemsText";
    private const string COINS_KEY = "coinsText";
    private const string HEALTH_BAR_KEY = "healthBar";

    [SerializeField] private TextMeshProUGUI redGemsText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Slider healthBar;      
               
    private void Awake()
    {
        DontDestroyOnLoad(this);    
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

    public object CaptureState()
    {
        Dictionary<string, object> data = new()
        {
            [RED_GEMS_KEY] = redGemsText.text,
            [COINS_KEY] = coinsText.text,
            [HEALTH_BAR_KEY] = healthBar.value,            
        };
        return data;
    }

    public void RestoreState(object state)
    {
        Dictionary<string, object> data = (Dictionary<string, object>)state;
        redGemsText.text = (string)data[RED_GEMS_KEY];
        coinsText.text = (string)data[COINS_KEY];
        healthBar.value = (float)data[HEALTH_BAR_KEY];
        gameObject.SetActive(true);
    }
}
