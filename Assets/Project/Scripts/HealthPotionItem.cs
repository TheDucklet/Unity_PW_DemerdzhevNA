using UnityEngine;

/// <summary>
/// Конкретная реализация предмета "Зелье Здоровья". Наследуется от CollectableItem.
/// </summary>
public class HealthPotionItem : CollectableItem
{
    [SerializeField] private float healAmount = 25f;

    private void Reset()
    {
        // Устанавливаем название предмета по умолчанию для новых объектов в инспекторе
        itemName = "Зелье Здоровья"; 
    }

    private void Awake()
    {
        // Если имя осталось базовым (например, при динамическом спавне), переопределяем его
        if (itemName == "Unnamed Item")
        {
            itemName = "Зелье Здоровья";
        }
    }
    
    protected override void OnCollect(PlayerStats player)
    {
        // Полиморфное поведение: восстанавливает здоровье
        player.Heal(healAmount);
    }
}