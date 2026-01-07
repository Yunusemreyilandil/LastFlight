using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdFlightController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7f;
    public float acceleration = 10f;

    [Header("Idle Vertical Float")]
    public float idleFloatAmplitude = 0.08f;
    public float idleFloatSpeed = 2f;

    [Header("Direction Sprites")]
    public Sprite upSprite;
    public Sprite downSprite;
    public Sprite leftSprite;
    public Sprite rightSprite;

    Rigidbody2D rb;
    Vector2 input;

    float idleTime;
    Vector3 visualStartLocalPos;

    [SerializeField] Transform visual;
    SpriteRenderer sr;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        sr = visual.GetComponent<SpriteRenderer>();

        visualStartLocalPos = visual.localPosition;
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();

        UpdateDirectionSprite();
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

    void UpdateDirectionSprite()
    {
        if (input == Vector2.zero) return;

        if (Mathf.Abs(input.x) > Mathf.Abs(input.y))
        {
            sr.sprite = input.x > 0 ? rightSprite : leftSprite;
        }
        else
        {
            sr.sprite = input.y > 0 ? upSprite : downSprite;
        }
    }
}
