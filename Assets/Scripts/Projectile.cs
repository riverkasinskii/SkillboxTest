using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : MonoBehaviour
{
    [SerializeField] private int damage = 5;
    [SerializeField] private float speed = 5f;
    [SerializeField] private float projectileLifeTime = 3f;

    private Player player;
    private Rigidbody2D myRigidbody2D;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        Vector2 direction = new Vector2(player.transform.localScale.x, 0);
        myRigidbody2D.AddForce(direction * speed, ForceMode2D.Impulse);
        StartCoroutine(LifeTimeProjectile());
    }
        
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.TryGetComponent(out Enemy enemy);
        if (enemy != null && collision.gameObject.CompareTag("Enemy"))
        {
            enemy.TakeDamage(damage);
            gameObject.SetActive(false);
        }        
    }        

    private IEnumerator LifeTimeProjectile()
    {        
        yield return new WaitForSeconds(projectileLifeTime);
        gameObject.SetActive(false);
    }
}
