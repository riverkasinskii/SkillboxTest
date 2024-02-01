using UnityEngine;
using UnityEngine.InputSystem;

public class Player : Character
{    
    [SerializeField] private float jumpSpeed = 5f;
    [SerializeField] private float climbSpeed = 5f;
            
    private BoxCollider2D myFeetCollider;
    private float gravityScaleAtStart;
    private Vector2 moveInput;
        

    protected override void Awake()
    {
        base.Awake();        
        myFeetCollider = GetComponent<BoxCollider2D>();
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
        
        if (!myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))        
            return;        

        if (inputValue.isPressed)
        {
            myRigidbody.velocity += new Vector2(0f, jumpSpeed);
        }
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
}
