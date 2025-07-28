using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public GameObject PauseMenu;


    /// <summary>
    /// Перезапускает сцену
    /// </summary>
    public void RestartScene()
    {
        TimerManager.Instance?.ResetTimer(); // Перезапускаем таймер
        ScoreManager.Instance?.ResetScore(); // Сбрасываем счет
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Pause()
    {
        PauseMenu.SetActive(true);

        Time.timeScale = 0.0f;

    }

    public void Play()
    {
        PauseMenu.SetActive(false);

        Time.timeScale = 1.0f;

    }


}
