using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (animator == null) return;

        // 1. Проверяем, нажата ли хотя бы одна из клавиш движения (W, A, S, D / Стрелки)
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveZ = Input.GetAxisRaw("Vertical");

        // Если игрок зажимает клавиши, moveInput будет равен 1, иначе 0
        bool isMoving = (Mathf.Abs(moveX) > 0.1f || Mathf.Abs(moveZ) > 0.1f);
        float currentSpeed = isMoving ? 1.0f : 0.0f;

        // Передаем значение в Animator
        animator.SetFloat("Speed", currentSpeed);

        // 2. Обработка атаки по ЛКМ
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("Attack");

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.PlayButtonClickSound();
            }
        }
    }
}