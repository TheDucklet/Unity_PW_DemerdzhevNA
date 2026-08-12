using UnityEngine;

/// <summary>
/// Класс хранит и инкапсулирует состояние персонажа (очки, здоровье).
/// </summary>
public class PlayerStats : MonoBehaviour
{
    // Инкапсулированные приватные поля
    [SerializeField] private int currentScore = 0;
    [SerializeField] private float currentHealth = 100f;
    [SerializeField] private float maxHealth = 100f;

    // Публичные свойства только для чтения (Getter)
    public int Score => currentScore;
    public float Health => currentHealth;
    public float MaxHealth => maxHealth;

    /// <summary>
    /// Безопасный метод добавления очков
    /// </summary>
    public void AddScore(int amount)
    {
        if (amount <= 0) return;
        currentScore += amount;
        Debug.Log($"[PlayerStats] Очки увеличены на {amount}. Всего: {currentScore}");
    }

    /// <summary>
    /// Безопасный метод восстановления здоровья
    /// </summary>
    public void Heal(float amount)
    {
        if (amount == 0) return;
    
        if (currentHealth <= 0 && amount < 0)
        {
            Debug.Log("<color=red>[Player]</color> Игрок уже погиб! Текущее здоровье: 0");
            return;
        }
    
        currentHealth = Mathf.Clamp(currentHealth + amount, 0, maxHealth);
        Debug.Log($"<color=cyan>[Player]</color> Здоровье изменено: {amount}. Текущее здоровье: {currentHealth}");
    
        if (currentHealth <= 0)
        {
            Debug.Log("<color=red>[Player]</color> Игрок потерял все очки здоровья!");
        }
    }
}