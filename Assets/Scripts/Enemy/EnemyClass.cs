using UnityEngine;
using UnityEngine.UIElements;

public class EnemyClass : MonoBehaviour
{
    [SerializeField] public int CurrentHP;
    [SerializeField] public GameObject HitPos;
    [SerializeField] protected float searchRange = 10f;
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected Transform player;
    [SerializeField] public EnemyStatus status;
    [SerializeField] private bool OnDeath;

    protected Animator animator;

    protected virtual void Start()
    {
        StatusSet();
        animator = GetComponent<Animator>();
    }

    protected virtual void Update()
    {
        if (!OnDeath)
        {
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
        }       
    }

    protected virtual void StatusSet()
    {
        CurrentHP = status.MaxHP;
    }

    protected virtual void Death()
    {
        animator.SetTrigger("Death");
        Destroy(gameObject, 3f);
    }
}
