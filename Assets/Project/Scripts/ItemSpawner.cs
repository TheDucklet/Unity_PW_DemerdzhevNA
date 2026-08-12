using UnityEngine;

/// <summary>
/// Скрипт процедурной генерации предметов в трехмерной области.
/// </summary>
public class ItemSpawner : MonoBehaviour
{
    [Header("Префабы для генерации")]
    [SerializeField] private GameObject[] itemPrefabs;

    [Header("Параметры спавна")]
    [SerializeField] private int itemsToSpawnCount = 10;
    [SerializeField] private Vector2 spawnAreaMin = new Vector2(-8f, -8f);
    [SerializeField] private Vector2 spawnAreaMax = new Vector2(8f, 8f);
    [SerializeField] private float spawnHeightY = 0.5f;

    private void Start()
    {
        SpawnAllItems();
    }

    /// <summary>
    /// Метод процедурного создания объектов на сцене
    /// </summary>

    private void SpawnAllItems()
    {
        if (itemPrefabs == null || itemPrefabs.Length == 0)
        {
            Debug.LogWarning("[ItemSpawner] Не назначены префабы для генерации!");
            return;
        }

        for (int i = 0; i < itemsToSpawnCount; i++)
        {
            // 1. Выбираем случайный префаб из массива
            GameObject selectedPrefab = itemPrefabs[Random.Range(0, itemPrefabs.Length)];

            // 2. Генерируем случайные 3D координаты
            float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
            float randomZ = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
            Vector3 spawnPosition = new Vector3(randomX, spawnHeightY, randomZ);

            // 3. Создаем объект на сцене
            GameObject spawnedItem = Instantiate(selectedPrefab, spawnPosition, Quaternion.identity);

            // 4. Привязываем к родителю для поддержания чистоты иерархии сцены
            spawnedItem.transform.SetParent(transform);
        }

        Debug.Log($"[ItemSpawner] Успешно сгенерировано {itemsToSpawnCount} объектов.");
    }
}