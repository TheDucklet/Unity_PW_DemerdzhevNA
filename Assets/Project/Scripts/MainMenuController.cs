using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// Класс управляет логикой Главного Меню, отображением сохраненных рекордов 
/// и загрузкой игровой сцены.
/// </summary>
public class MainMenuController : MonoBehaviour
{
    [Header("UI Тексты")]
    [SerializeField] private TMP_Text scoreRecordText;

    private void Start()
    {
        // Отображаем переданные из прошлой сцены данные
        if (scoreRecordText != null)
        {
            scoreRecordText.text = $"Последний результат: {GameData.LastScore}\nРекорд: {GameData.HighScore}";
        }
    }

    public void PlayGame()
    {
        // Загружаем основную геймплейную сцену по имени
        SceneManager.LoadScene("MainScene");
    }

    public void ExitGame()
    {
        Debug.Log("[MainMenu] Выполнение выхода из приложения...");
        Application.Quit();
    }
}