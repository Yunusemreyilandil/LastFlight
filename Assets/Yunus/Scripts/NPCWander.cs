using UnityEngine;

public class NPCWander : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 1.5f;
    public float stopDistance = 0.05f;

    [Header("Timers")]
    public float walkTime = 2f;
    public float waitTime = 1.5f;

    [Header("Walk Area")]
    public Collider2D walkArea;

    private Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer sr;

    private Vector2 targetPosition;
    private float timer;
    private bool isWalking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        PickRandomPoint();
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (isWalking)
        {
            if (Vector2.Distance(transform.position, targetPosition) <= stopDistance)
            {
                StartWaiting();
            }
        }
        else
        {
            if (timer <= 0)
            {
                PickRandomPoint();
            }
        }

        UpdateAnimation();
    }

    void FixedUpdate()
    {
        if (!isWalking)
        {
            rb.linearVelocity = Vector2.zero;
            return;
        }

        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;
        rb.linearVelocity = direction * moveSpeed;
    }

    void PickRandomPoint()
    {
        isWalking = true;
        timer = walkTime;

        Bounds b = walkArea.bounds;

        targetPosition = new Vector2(
            Random.Range(b.min.x, b.max.x),
            Random.Range(b.min.y, b.max.y)
        );
    }

    void StartWaiting()
    {
        isWalking = false;
        timer = waitTime;
        rb.linearVelocity = Vector2.zero;
    }

    void UpdateAnimation()
    {
        Vector2 velocity = rb.linearVelocity;

        animator.SetBool("isMoving", isWalking);
        animator.SetFloat("moveX", velocity.x);
        animator.SetFloat("moveY", velocity.y);

        
    }
}
