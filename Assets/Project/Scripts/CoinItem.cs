using UnityEngine;

/// <summary>
/// Конкретная реализация предмета "Монета". Наследуется от CollectableItem.
/// </summary>
public class CoinItem : CollectableItem
{
    [SerializeField] private int scoreReward = 10;

    private void Reset()
    {
        // Устанавливаем название предмета по умолчанию для новых объектов в инспекторе
        itemName = "Монета"; 
    }

    private void Awake()
    {
        // Если имя осталось базовым (например, при динамическом спавне), переопределяем его
        if (itemName == "Unnamed Item")
        {
            itemName = "Монета";
        }
    }
    protected override void OnCollect(PlayerStats player)
    {
        // Полиморфное поведение: начисляет очки
        player.AddScore(scoreReward);
    }
}