using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdFlightController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7f;
    public float acceleration = 10f;

    [Header("Idle Float")]
    public float idleFloatAmplitude = 0.08f;
    public float idleFloatSpeed = 2f;

    Rigidbody2D rb;
    Vector2 input;

    float idleTime;
    Vector3 visualStartLocalPos;

    [SerializeField] Transform visual;
    
    [SerializeField] SpriteRenderer sr;
    [SerializeField] Animator animator;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = visual.GetComponent<Animator>();
        visualStartLocalPos = visual.localPosition;
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();

        UpdateAnimator();
    }

    void FixedUpdate()
    {
        Vector2 targetVelocity = input * moveSpeed;

        rb.linearVelocity = Vector2.Lerp(
            rb.linearVelocity,
            targetVelocity,
            acceleration * Time.fixedDeltaTime
        );
    }

    void LateUpdate()
    {
        HandleIdleVerticalFloat();
    }

    void UpdateAnimator()
    {
        float absX = Mathf.Abs(input.x);
        float absY = Mathf.Abs(input.y);

        animator.SetFloat("MoveX", input.x);
        animator.SetFloat("MoveY", input.y);
        animator.SetBool("Horizontal", Mathf.Abs(input.x) > Mathf.Abs(input.y));

        if (input.x > 0.1f)
            sr.flipX = false;
        else if (input.x < -0.1f)
            sr.flipX = true;

        // yön önceliği kodda
        animator.SetBool("Horizontal", absX > absY);
    }

    void HandleIdleVerticalFloat()
    {
        if (rb.linearVelocity.magnitude < 0.1f)
        {
            idleTime += Time.deltaTime;
            float yOffset = Mathf.Sin(idleTime * idleFloatSpeed) * idleFloatAmplitude;
            visual.localPosition = visualStartLocalPos + new Vector3(0, yOffset, 0);
        }
        else
        {
            idleTime = 0f;
            visual.localPosition = Vector3.Lerp(
                visual.localPosition,
                visualStartLocalPos,
                10f * Time.deltaTime
            );
        }
    }
}
