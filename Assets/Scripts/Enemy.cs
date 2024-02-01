using UnityEngine;

public class Enemy : Character
{
    private float runSpeed;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        Walk();
    }

    protected override void Walk()
    {
        myRigidbody.velocity = new Vector2(walkSpeed, 0f);
        myAnimator.SetBool("isWalking", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        RunTowardPlayer(collision);
    }

    private void RunTowardPlayer(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            runSpeed = walkSpeed;
            walkSpeed = runSpeed * 2;
            myAnimator.SetBool("isRunning", true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        StopRun(collision);        
        FlipSprite();
    }

    private void StopRun(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            walkSpeed = runSpeed;
            myAnimator.SetBool("isRunning", false);
        }
    }

    protected override void FlipSprite()
    {
        walkSpeed = -walkSpeed;
        transform.localScale = new Vector2(-(Mathf.Sign(myRigidbody.velocity.x)), 1f);
    }
}
