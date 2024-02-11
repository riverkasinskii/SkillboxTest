using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D), typeof(Transform))]
public class Player : Character
{   
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private string projectileTag;
    [SerializeField] private int spikeDamage = 5;

    private BoxCollider2D myFeetCollider;
    private float gravityScaleAtStart;
    private Vector2 moveInput;
    private ProjectilePooler projectilePooler;
    private Transform shootPoint;
    private UI ui;

    protected override void Awake()
    {        
        base.Awake();        
        myFeetCollider = GetComponent<BoxCollider2D>();
        shootPoint = GetComponent<Transform>().GetChild(0); 
        ui = FindObjectOfType<UI>();        
    }

    private void Start()
    {
        projectilePooler = ProjectilePooler.Instance;
        gravityScaleAtStart = myRigidbody.gravityScale;
        ui.ResetHealthBar();
    }

    private void FixedUpdate()
    {
        if (!isAlive) 
            return;
        Walk();
        FlipSprite();
        ClimbLadder();        
    }
        
    /// <summary>
    /// Callback Unity InputSystem
    /// </summary>
    /// <param name="inputValue"></param>
    private void OnMove(InputValue inputValue)
    {
        if (!isAlive)        
            return;
        SetAttackState(false);
        moveInput = inputValue.Get<Vector2>();
    }

    /// <summary>
    /// Callback Unity InputSystem
    /// </summary>
    /// <param name="inputValue"></param>
    private void OnJump(InputValue inputValue)
    {
        if (!isAlive)        
            return;
        
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground", "Interactable")))        
            return;        

        if (inputValue.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
    }

    /// <summary>
    /// Callback Unity InputSystem
    /// </summary>
    /// <param name="inputValue"></param>
    private void OnFire(InputValue inputValue)
    {
        if (!isAlive)
            return;

        if (inputValue.isPressed)
        {
            SetAttackState(true);
        }
    }       
    
    /// <summary>
    /// PlayerAttack Animation Clip Event
    /// </summary>
    private void AttackStop()
    {
        SetAttackState(false);
    }

    protected override void SetAttackState(bool state)
    {
        myAnimator.SetBool("isAttack", state);
    }

    /// <summary>
    /// PlayerAttack Animation Clip Event
    /// </summary>
    private void Shoot()
    {
        projectilePooler.SpawnFromPool(projectileTag, shootPoint.position, transform.rotation);        
    }

    protected override void Walk()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        Vector2 playerVelocity = new(moveInput.x * walkSpeed * Time.fixedDeltaTime,
            myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        myAnimator.SetBool(WALKING_STATE, playerHasHorizontalSpeed);        
    }

    protected override void FlipSprite()
    {
        bool playerHasHorizontalSpeed = Mathf.Abs(myRigidbody.velocity.x) > Mathf.Epsilon;

        if (playerHasHorizontalSpeed)
        {
            transform.localScale = new Vector2(Mathf.Sign(myRigidbody.velocity.x), 1f);
        }
    }

    private void ClimbLadder()
    {        
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Climbing")))
        {
            myRigidbody.gravityScale = gravityScaleAtStart;            
            return;
        }
        Vector2 climbVelocity = new(myRigidbody.velocity.x, moveInput.y * climbSpeed);
        myRigidbody.velocity = climbVelocity;
        myRigidbody.gravityScale = 0f;        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Spike"))
        {
            TakeDamage(spikeDamage);
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        Stats.Health -= damage;
        ui.UpdateHealthBar(damage);
        print(currentHealth);
    }
}
