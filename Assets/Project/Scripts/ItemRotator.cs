using UnityEngine;

/// <summary>
/// Скрипт базового поведения трехмерного объекта: 
/// отвечает за визуальное вращение и инициализацию случайного цвета.
/// </summary>
public class ItemRotator : MonoBehaviour
{
    [Header("Настройки вращения")]
    [SerializeField] private Vector3 rotationSpeed = new Vector3(0f, 90f, 0f);

    [Header("Настройки визуала")]
    [SerializeField] private bool randomizeColorOnStart = true;

    private Renderer objectRenderer;

    private void Start()
    {
        // Получаем доступ к компоненту Renderer трехмерного объекта
        objectRenderer = GetComponent<Renderer>();

        if (randomizeColorOnStart && objectRenderer != null)
        {
            // Назначаем случайный цвет материалу объекта
            objectRenderer.material.color = Random.ColorHSV(0f, 1f, 0.8f, 1f, 0.8f, 1f);
        }

        Debug.Log($"[ItemRotator] Объект {gameObject.name} инициализирован на позиции {transform.position}");
    }

    private void Update()
    {
        // Плавно вращаем объект каждый кадр с учетом deltaTime
        transform.Rotate(rotationSpeed * Time.deltaTime);
    }
}