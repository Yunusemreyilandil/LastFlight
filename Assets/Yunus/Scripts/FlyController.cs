using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody2D))]
public class BirdFlightController : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 7f;
    public float acceleration = 10f;

    [Header("Idle Float")]
    public float idleFloatAmplitude = 0.08f;
    public float idleFloatSpeed = 2f;

    [Header("Stamina")]
    public float maxStamina = 5f;
    public float staminaDrainPerSecond = 1.5f;
    public float staminaRegenPerSecond = 3f;

    [Header("Stamina UI")]
    [SerializeField] Image staminaFill;
    public float staminaUISpeed = 6f;



    public float boostSpeedMultiplier = 1.5f;
    float currentStamina;
    bool isBoosting;

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

        currentStamina = maxStamina;
    }

    void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.Normalize();

        UpdateAnimator();
        HandleStamina();
        
    }

    void FixedUpdate()
    {
        float finalSpeed = moveSpeed;

        if (isBoosting)
            finalSpeed *= boostSpeedMultiplier;

        Vector2 targetVelocity = input * finalSpeed;

      

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

            animator.SetFloat("MoveX", input.x);
            animator.SetFloat("MoveY", input.y);
            animator.SetBool("Horizontal", Mathf.Abs(input.x) > Mathf.Abs(input.y));

            if (input.x > 0.1f)
                sr.flipX = false;
            else if (input.x < -0.1f)
                sr.flipX = true;


        animator.SetBool("Boost", isBoosting);

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
        void HandleStamina()
        {
            bool shiftPressed = Input.GetKey(KeyCode.LeftShift);
            if (shiftPressed & currentStamina > 0f && input.magnitude > 0.1f)
            {
                isBoosting = true;
                currentStamina -= staminaDrainPerSecond * Time.deltaTime;

            }
            else
            {
                isBoosting = false;
                currentStamina += staminaRegenPerSecond * Time.deltaTime;
            }

            currentStamina = Mathf.Clamp(currentStamina, 0f, maxStamina);

        UpdateStaminaUI();

    }

    void UpdateStaminaUI()
    {
        if (staminaFill == null) return;

        float target = currentStamina / maxStamina;
        staminaFill.fillAmount = Mathf.Lerp(
            staminaFill.fillAmount,
            target,
            staminaUISpeed * Time.deltaTime
        );
    }
}

