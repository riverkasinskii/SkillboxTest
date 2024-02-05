using System.Collections;
using UnityEngine;

public class Enemy : Character
{
    [SerializeField] protected int damage = 10;

    private bool isAttack = false;
    private const string TARGET_TAG = "Player";
    private float timeBetweenAttacks = 1f;

    protected override void Awake()
    {        
        base.Awake();        
    }

    private void Update()
    {
        if (isAttack)
            return;
        Walk();
    }

    protected override void Walk()
    {
        myRigidbody.velocity = new Vector2(walkSpeed, 0f);
        myAnimator.SetBool("isWalking", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        AttackPlayer(collision);
    }

    private void AttackPlayer(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(TARGET_TAG))
        {
            bool distance = Vector2.Distance(collision.gameObject.transform.position,
            gameObject.transform.position) <= 1f;
            if (distance)
            {
                SetAttackState(true);
                StartCoroutine(CauseDamage(collision, damage));
                myRigidbody.velocity = new Vector2(0f, 0f);
            }                                   
        }
    }

    private IEnumerator CauseDamage(Collider2D collision, int damage)
    {
        bool isPlayer = collision.TryGetComponent(out Player player);
        while (player.gameObject.activeSelf)
        {
            if (isPlayer)
            {
                player.TakeDamage(damage);
            }
            yield return new WaitForSeconds(timeBetweenAttacks);
        }        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SetAttackState(false);
        if (collision.gameObject.CompareTag("Ground"))
        {
            FlipSprite();
        }        
    }

    private void SetAttackState(bool state)
    {
        isAttack = state;
        myAnimator.SetBool("isAttack", isAttack);
    }

    protected override void FlipSprite()
    {
        walkSpeed = -walkSpeed;
        transform.localScale = new Vector2(-Mathf.Sign(myRigidbody.velocity.x), 1f);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        print(currentHealth);
    }
}
