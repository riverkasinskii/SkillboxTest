using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider2D), typeof(Transform))]
public class Player : Character
{
    public static Player Instance;

    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
    [SerializeField] private string projectileTag;    

    private BoxCollider2D myFeetCollider;
    private float gravityScaleAtStart;
    private Vector2 moveInput;
    private ProjectilePooler projectilePooler;
    private Transform shootPoint;

    protected override void Awake()
    {
        Instance = this;
        base.Awake();
        projectilePooler = ProjectilePooler.Instance;
        myFeetCollider = GetComponent<BoxCollider2D>();
        shootPoint = GetComponent<Transform>().GetChild(0);
    }

    private void Start()
    {                
        gravityScaleAtStart = myRigidbody.gravityScale;
    }

    private void Update()
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
        myAnimator.SetBool("isAttack", false);
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
        
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Interactable")))        
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
            myAnimator.SetBool("isAttack", true);                       
        }
    }       
    
    /// <summary>
    /// PlayerAttack Animation Clip Event
    /// </summary>
    private void AttackStop()
    {
        myAnimator.SetBool("isAttack", false);
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

        Vector2 playerVelocity = new(moveInput.x * walkSpeed, myRigidbody.velocity.y);
        myRigidbody.velocity = playerVelocity;
        myAnimator.SetBool("isWalking", playerHasHorizontalSpeed);        
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

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        print(currentHealth);
    }
}
