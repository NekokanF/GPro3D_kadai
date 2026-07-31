using UnityEngine;
using UnityEngine.UIElements;

public class EnemyClass : MonoBehaviour
{
    [SerializeField] public int CurrentHP;
    [SerializeField] public GameObject HitPos;
    [SerializeField] public GameObject AttackPos;
    [SerializeField] protected float searchRange = 10f;
    [SerializeField] protected float attackRange = 3f;
    [SerializeField] public float moveSpeed;
    [SerializeField] public float BaseMoveSpeed;
    [SerializeField] protected Transform player;
    [SerializeField] public EnemyStatus status;
    [SerializeField] private bool OnDeath;
    [SerializeField] public bool OnAttack;

    protected Animator animator;
    CapsuleCollider capsuleCollider;
    Rigidbody rb;

    protected virtual void Start()
    {
        StatusSet();
        moveSpeed = BaseMoveSpeed;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider>();
    }

    protected virtual void Update()
    {
        if (!OnDeath)
        {
            // move
            if (Vector3.Distance(transform.position, player.position) <= searchRange)
            {
                animator.SetBool("Move", true);
                Vector3 direction = (player.position - transform.position).normalized;
                direction.y = 0;

                transform.position += direction * moveSpeed * Time.deltaTime;

                if (direction.sqrMagnitude > 0.001f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);

                    transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        targetRotation,
                        Time.deltaTime * 5f
                    );
                }
            }
            else
            {
                animator.SetBool("Move", false);
            }

            if (CurrentHP <= 0)
            {
                OnDeath = true;
                Death();
            }

            if (Vector3.Distance(transform.position, player.position) <= attackRange && !OnAttack)
            {
                OnAttack = true;
                animator.SetTrigger("Attack");
                moveSpeed = moveSpeed * 0.8f;
            }
        }       
    }

    protected virtual void StatusSet()
    {
        CurrentHP = status.MaxHP;
    }

    protected virtual void Death()
    {
        AttackPos.SetActive(false);
        rb.useGravity = false;
        capsuleCollider.enabled = false;
        animator.SetTrigger("Death");
        Destroy(gameObject, 5f);
    }
}
