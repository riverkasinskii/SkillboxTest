using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI blueGemsText;
    [SerializeField] private TextMeshProUGUI redGemsText;
    [SerializeField] private TextMeshProUGUI coinsText;
    [SerializeField] private Slider healthBar;

    private void Start()
    {
        healthBar.value = 1f;
    }

    public void UpdateBlueGems()
    {
        blueGemsText.text = Stats.BlueGems.ToString("D3");
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
}
