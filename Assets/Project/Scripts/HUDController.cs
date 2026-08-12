using UnityEngine;
using UnityEngine.UI;
using TMPro;

/// <summary>
/// Класс отвечает за динамическое обновление шкал здоровья, 
/// выносливости и счетчика очков в реальном времени.
/// </summary>
public class HUDController : MonoBehaviour
{
    [Header("UI Компоненты")]
    [SerializeField] private Slider healthSlider;
    [SerializeField] private Slider staminaSlider;
    [SerializeField] private TMP_Text scoreText;

    [Header("Ссылки на сущности игрока")]
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerController playerController;

    private void Start()
    {
        // Автоматический поиск сущностей, если они не заданы в инспекторе
        if (playerStats == null)
        {
            playerStats = FindAnyObjectByType<PlayerStats>();
        }

        if (playerController == null)
        {
            playerController = FindAnyObjectByType<PlayerController>();
        }
    }

    private void Update()
    {
        UpdateHealthUI();
        UpdateStaminaUI();
        UpdateScoreUI();
    }

    private void UpdateHealthUI()
    {
        if (playerStats != null && healthSlider != null)
        {
            // Нормализуем значение здоровья от 0 до 1
            healthSlider.value = playerStats.Health / playerStats.MaxHealth;
        }
    }

    private void UpdateStaminaUI()
    {
        if (playerController != null && staminaSlider != null)
        {
            // Нормализуем значение выносливости от 0 до 1
            staminaSlider.value = playerController.CurrentStamina / playerController.MaxStamina;
        }
    }

    private void UpdateScoreUI()
    {
        if (playerStats != null && scoreText != null)
        {
            scoreText.text = $"Сокровища: {playerStats.Score}";
        }
    }
}