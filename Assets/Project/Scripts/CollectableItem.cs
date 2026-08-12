using UnityEngine;

[RequireComponent(typeof(Collider))]
public abstract class CollectableItem : MonoBehaviour
{
    [Header("Базовые параметры")]
    [SerializeField] protected string itemName = "Предмет";
    [SerializeField] protected GameObject collectEffectPrefab; // Префаб частиц CollectFX

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerStats player = other.GetComponent<PlayerStats>();
            if (player != null)
            {
                OnCollect(player);

                // Воспроизведение звука сбора
                if (AudioManager.Instance != null)
                    AudioManager.Instance.PlayCoinSound();

                // Создание вспышки частиц
                if (collectEffectPrefab != null)
                {
                    GameObject fx = Instantiate(collectEffectPrefab, transform.position, Quaternion.identity);
                    Destroy(fx, 1.5f);
                }

                Debug.Log($"<color=yellow>[Item]</color> Собран предмет: {itemName}");
                Destroy(gameObject);
            }
        }
    }

    protected abstract void OnCollect(PlayerStats player);
}