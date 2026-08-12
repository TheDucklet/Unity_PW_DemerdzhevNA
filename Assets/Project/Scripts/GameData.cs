/// <summary>
/// Статический класс для сохранения игровых данных (очков, рекордов) 
/// при переходах между независимыми сценами.
/// </summary>
public static class GameData
{
    public static int LastScore = 0;
    public static int HighScore = 0;

    public static void SaveScore(int currentScore)
    {
        LastScore = currentScore;
        if (currentScore > HighScore)
        {
            HighScore = currentScore;
        }
    }
}