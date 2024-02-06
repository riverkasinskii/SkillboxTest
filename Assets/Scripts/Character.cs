using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public abstract class Character : MonoBehaviour, IAttackable
{
    [SerializeField] protected float walkSpeed = 100f;
    [SerializeField] protected float currentHealth;    

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

    public virtual void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);            
        }
    }
}
