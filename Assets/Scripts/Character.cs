using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public abstract class Character : MonoBehaviour, IAttackable
{
    [SerializeField] protected float walkSpeed = 100f;
    [SerializeField] protected float currentHealth;

    protected const string ATTACKING_STATE = "isAttack";
    protected const string WALKING_STATE = "isWalking";

    protected Rigidbody2D myRigidbody;
    protected Animator myAnimator;    

    protected bool isAlive = true;

    protected virtual void Awake()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        Stats.Health = currentHealth;
    }

    protected abstract void Walk();

    protected abstract void FlipSprite();

    protected abstract void SetAttackState(bool state);    

    public virtual void TakeDamage(float damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            gameObject.SetActive(false);
            isAlive = false;
        }
    }
}
