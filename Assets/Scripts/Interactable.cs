using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private AudioClip coinPickupSFX;
    [SerializeField] private AudioClip gemPickupSFX;

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
                AudioSource.PlayClipAtPoint(gemPickupSFX, Camera.main.transform.position);
                ui.UpdateRedGems();
                wasCollected = true;
            }
            else if(gameObject.CompareTag("Coin") && !wasCollected)
            {
                Stats.Coins += 1;
                AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position);
                ui.UpdateCoins();
                wasCollected = true;
            }
            gameObject.SetActive(false);
        }
    }
}
