using UnityEngine;

public class Enemy : Character
{
    [SerializeField] private HealthBar healthBar;

    private const string TARGET_TAG = "Player";
    private const string RUNNING_STATE = "isRunning";
    private const float DISTANCE_TO_TARGET = 0.8f;

    private Player player;
    private bool isAttack = false;

    protected override void Awake()
    {        
        base.Awake();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        myAnimator.SetBool(WALKING_STATE, true);        
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

    private void OnTriggerStay2D(Collider2D collision)
    {        
        if (collision.gameObject.CompareTag(TARGET_TAG))
        {
            SetRunState(true);
            bool distance = Vector2.Distance(collision.gameObject.transform.position,
            gameObject.transform.position) <= DISTANCE_TO_TARGET;
            
            if (distance)
            {
                AttackPlayer();
            }            
        }
    }
        
    private void OnTriggerExit2D(Collider2D collision)
    {
        SetAttackState(false);
        SetRunState(false);
        if (collision.gameObject.CompareTag("Ground"))
        {
            FlipSprite();
        }
    }

    private void AttackPlayer()
    {
        SetAttackState(true);
        SetRunState(false);
        myRigidbody.velocity = new Vector2(0f, 0f);
    }

    /// <summary>
    /// EnemyAttack Animation Clip Event, damage = 10
    /// </summary>
    private void CauseDamage(int damage)
    {
        player.TakeDamage(damage);
    }

    protected override void SetAttackState(bool state)
    {
        isAttack = state;
        myAnimator.SetBool(ATTACKING_STATE, isAttack);
    }

    private void SetRunState(bool state)
    {
        myAnimator.SetBool(RUNNING_STATE, state);
    }

    protected override void FlipSprite()
    {
        walkSpeed = -walkSpeed;
        transform.localScale = new Vector2(-Mathf.Sign(myRigidbody.velocity.x), 1f);
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        healthBar.SetHealthValue(currentHealth, maxHealth);
        print(currentHealth);
    }
}
