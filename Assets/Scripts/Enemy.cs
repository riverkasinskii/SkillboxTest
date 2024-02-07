using UnityEngine;

public class Enemy : Character
{    
    private bool isAttack = false;    
    private const string TARGET_TAG = "Player";
    private Player player;    

    protected override void Awake()
    {        
        base.Awake();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        myAnimator.SetBool("isWalking", true);        
    }

    private void FixedUpdate()
    {
        if (isAttack)
            return;
        Walk();
    }

    protected override void Walk()
    {
        myRigidbody.velocity = new Vector2(walkSpeed * Time.fixedDeltaTime, 0f);        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.CompareTag(TARGET_TAG))
        {
            RunToPlayer();
            bool distance = Vector2.Distance(collision.gameObject.transform.position,
            gameObject.transform.position) <= 1f;
            if (distance)
            {
                AttackPlayer();
            }            
        }
    }

    private void RunToPlayer()
    {               
        myAnimator.SetBool("isRunning", true);        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        SetAttackState(false);
        myAnimator.SetBool("isRunning", false);        
        if (collision.gameObject.CompareTag("Ground"))
        {
            FlipSprite();
        }
    }

    private void AttackPlayer()
    {
        SetAttackState(true);
        myRigidbody.velocity = new Vector2(0f, 0f);
    }

    /// <summary>
    /// EnemyAttack Animation Clip Event, damage = 10
    /// </summary>
    private void CauseDamage(int damage)
    {
        player.TakeDamage(damage);
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

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        print(currentHealth);
    }
}
