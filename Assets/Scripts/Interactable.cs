using UnityEngine;

public class Interactable : MonoBehaviour
{
    private UI ui;

    private void Awake()
    {
        ui = FindObjectOfType<UI>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (gameObject.CompareTag("BlueGem"))
            {
                Stats.BlueGems += 1;
                ui.UpdateBlueGems();
            }
            else if(gameObject.CompareTag("RedGem"))
            {
                Stats.RedGems += 1;
                ui.UpdateRedGems();
            }
            else if (gameObject.CompareTag("Coin"))
            {
                Stats.Coins += 1;
                ui.UpdateCoins();
            }
            gameObject.SetActive(false);
        }
    }
}
