using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class Barrel : MonoBehaviour
{
    [SerializeField] private int hitsBeforeExplosion = 3;
    [SerializeField] private float barrelLifeTime = 0.5f;

    private int barrelDamage;
    private int BarrelDamage { get => barrelDamage * 10; }

    private CircleCollider2D circleCollider;

    private void Awake()
    {
        circleCollider = GetComponent<CircleCollider2D>();
    }

    private void Start()
    {
        barrelDamage = hitsBeforeExplosion;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.gameObject.TryGetComponent(out Projectile projectile);
        if (projectile != null)
        {
            hitsBeforeExplosion--;
            projectile.gameObject.SetActive(false);
            if (hitsBeforeExplosion == 0)
            {
                circleCollider.radius = 5;
                StartCoroutine(BarrelLifeTime());
            }
        }
        if (hitsBeforeExplosion <= 0)
        {
            CauseDamage(collision);
        }        
    }

    private void CauseDamage(Collision2D collision)
    {
        bool target = collision.gameObject.CompareTag("Player") ||
            collision.gameObject.CompareTag("Enemy");

        if (target)
        {
            collision.gameObject.TryGetComponent(out IAttackable character);
            character.TakeDamage(BarrelDamage);
        }        
    }
        
    private IEnumerator BarrelLifeTime()
    {
        yield return new WaitForSeconds(barrelLifeTime);
        gameObject.SetActive(false);
    }           
}
