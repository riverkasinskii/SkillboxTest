using UnityEngine;

public class Interactable : MonoBehaviour
{
    private UI ui;
    private bool wasCollected = false;

    private void Awake()
    {
        ui = FindObjectOfType<UI>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {            
            if(gameObject.CompareTag("RedGem") && !wasCollected)
            {
                Stats.RedGems += 1;
                ui.UpdateRedGems();
                wasCollected = true;
            }
            else if(gameObject.CompareTag("Coin") && !wasCollected)
            {
                Stats.Coins += 1;
                ui.UpdateCoins();
                wasCollected = true;
            }
            gameObject.SetActive(false);
        }
    }
}
