using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] protected float walkSpeed = 1f;

    protected Rigidbody2D myRigidbody;
    protected Animator myAnimator;    

    protected readonly bool isAlive = true;

    protected virtual void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();        
    }

    protected abstract void Walk();

    protected abstract void FlipSprite();
}
