using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Параметры Скорости")]
    [SerializeField] private float walkSpeed = 5.0f;
    [SerializeField] private float sprintSpeed = 9.0f;
    [SerializeField] private float rotationSpeed = 12.0f;

    [Header("Параметры Выносливости (Stamina)")]
    [SerializeField] private float maxStamina = 100.0f;
    [SerializeField] private float currentStamina = 100.0f;
    [SerializeField] private float staminaDrain = 25.0f;
    [SerializeField] private float staminaRegen = 15.0f;

    public float CurrentStamina => currentStamina;
    public float MaxStamina => maxStamina;

    private Rigidbody rb;
    private Vector3 moveInput;
    private float currentSpeed;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        currentSpeed = walkSpeed;
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        moveInput = new Vector3(horizontal, 0f, vertical).normalized;

        // Управление спринтом и выносливостью
        HandleStamina();
    }

    private void HandleStamina()
    {
        bool wantToSprint = Input.GetKey(KeyCode.LeftShift) && moveInput.magnitude > 0.1f;

        if (wantToSprint && currentStamina > 0)
        {
            currentSpeed = sprintSpeed;
            currentStamina -= staminaDrain * Time.deltaTime;
            currentStamina = Mathf.Max(currentStamina, 0);
            Debug.Log($"<color=yellow>[Sprint]</color> Спринт активен! Выносливость: {currentStamina:F1}");
        }
        else
        {
            currentSpeed = walkSpeed;

            if (currentStamina < maxStamina)
            {
                currentStamina += staminaRegen * Time.deltaTime;
                currentStamina = Mathf.Min(currentStamina, maxStamina);
            }
        }
    }

    private void FixedUpdate()
    {
        if (moveInput.magnitude > 0.1f)
        {
            Vector3 targetPosition = rb.position + moveInput * currentSpeed * Time.fixedDeltaTime;
            rb.MovePosition(targetPosition);

            Quaternion targetRotation = Quaternion.LookRotation(moveInput);
            rb.rotation = Quaternion.Slerp(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }
}