using UnityEngine;

/// <summary>
/// Продвинутый скрипт плавного следования камеры за целью (обычно за игроком).
/// Обеспечивает плавность, независимую от частоты кадров (FPS).
/// </summary>
public class CameraFollow : MonoBehaviour
{
    [Header("Target Settings")]
    [Tooltip("Объект, за которым следует камера. Если не указан, выполнится поиск по тегу 'Player'.")]
    [SerializeField] private Transform target;

    [Header("Position & Movement")]
    [Tooltip("Смещение камеры относительно цели (X - лево/право, Y - высота, Z - назад/вперед).")]
    [SerializeField] private Vector3 offset = new Vector3(0f, 8f, -7f);

    [Tooltip("Скорость плавного догоняющего движения (Smooth Time).")]
    [SerializeField, Range(0.1f, 30f)] private float smoothSpeed = 10f;

    [Header("Rotation (Look At)")]
    [Tooltip("Смещение точки, на которую смотрит камера (обычно немного выше плеч персонажа).")]
    [SerializeField] private Vector3 lookAtOffset = new Vector3(0f, 1f, 0f);
    
    [Tooltip("Должна ли камера автоматически поворачиваться на цель?")]
    [SerializeField] private bool useLookAt = true;

    // Внутренняя переменная для хранения текущей скорости движения камеры.
    // Необходима для корректной работы SmoothDamp.
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        TryFindTarget();
    }

    private void LateUpdate()
    {
        // Если цель потеряна (например, игрок умер), пытаемся найти её заново.
        // Это спасает ситуацию при респавне персонажа.
        if (target == null)
        {
            TryFindTarget();
            if (target == null) return;
        }

        // 1. Вычисляем желаемую позицию
        Vector3 desiredPosition = target.position + offset;

        // 2. Плавное перемещение камеры
        // Vector3.SmoothDamp математически корректно обеспечивает плавное затухание (easing),
        // которое полностью НЕ ЗАВИСИТ от FPS (в отличие от Lerp с Time.deltaTime).
        transform.position = Vector3.SmoothDamp(
            current: transform.position,
            target: desiredPosition,
            currentVelocity: ref velocity,
            smoothTime: 1f / smoothSpeed,
            maxSpeed: Mathf.Infinity,
            deltaTime: Time.deltaTime
        );

        // 3. Поворот камеры
        if (useLookAt)
        {
            transform.LookAt(target.position + lookAtOffset);
        }
    }

    /// <summary>
    /// Позволяет внешним скриптам переназначить цель камеры.
    /// Вызывайте это, если игнок респавнился и создался новый GameObject.
    /// </summary>
    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
        velocity = Vector3.zero; // Сбрасываем скорость, чтобы камера не "дернулась" при переходе
    }

    /// <summary>
    /// Безопасный поиск цели по тегу.
    /// </summary>
    private void TryFindTarget()
    {
        if (target != null) return;

        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            target = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("[CameraFollow] Объект с тегом 'Player' не найден на сцене!");
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// Отрисовка вспомогательных линий (Gizmos) в редакторе Unity.
    /// Помогает визуально настраивать камеру, не запуская игру.
    /// </summary>
    private void OnDrawGizmosSelected()
    {
        if (target != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, target.position);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(target.position + lookAtOffset, 0.2f);
        }
    }
#endif
}