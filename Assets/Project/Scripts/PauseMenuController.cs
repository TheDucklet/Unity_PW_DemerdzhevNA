using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Управление интерактивным меню паузы, остановкой времени 
/// и переходами по кнопкам.
/// </summary>
public class PauseMenuController : MonoBehaviour
{
    [Header("Панель Паузы")]
    [SerializeField] private GameObject pauseMenuPanel;

    private bool isPaused = false;

    private void Update()
    {
        // Открытие/закрытие паузы при нажатии Escape
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    /// <summary>
    /// Возобновление игры
    /// </summary>
    public void Resume()
    {
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(false);

        Time.timeScale = 1.0f; // Возвращаем нормальную скорость времени
        isPaused = false;
    }

    /// <summary>
    /// Остановка игры и показ меню
    /// </summary>
    public void Pause()
    {
        if (pauseMenuPanel != null)
            pauseMenuPanel.SetActive(true);

        Time.timeScale = 0.0f; // Полностью останавливаем физику и таймеры
        isPaused = true;
    }

    /// <summary>
    /// Перезапуск текущей сцены
    /// </summary>
    public void RestartGame()
    {
        Time.timeScale = 1.0f; // Обязательно возвращаем время перед перезагрузкой
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Переход в Главное Меню
    /// </summary>
    public void LoadMainMenu()
{
    Time.timeScale = 1.0f;

    // Сохраняем текущие очки перед сменной сцены
    PlayerStats player = FindAnyObjectByType<PlayerStats>();
    if (player != null)
    {
        GameData.SaveScore(player.Score);
    }

    SceneManager.LoadScene("MainMenuScene");
}
}