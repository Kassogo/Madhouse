using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Madhouse.BipolarDisorder
{
    public class GameOverPanel : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TextMeshProUGUI _gameOverText;

        private void Start()
        {
            _gameOverPanel.SetActive(false); // Скрываем панель при старте

            if (LifeManager.Instance != null)
            {
                LifeManager.Instance.OnLivesChanged += CheckGameOver;
            }
        }

        private void OnDestroy()
        {
            if (LifeManager.Instance != null)
            {
                LifeManager.Instance.OnLivesChanged -= CheckGameOver;
            }
        }

        private void CheckGameOver(int lives)
        {
            if (lives <= 0)
            {
                ShowGameOver();
            }
        }

        private void ShowGameOver()
        {
            _gameOverPanel.SetActive(true);
            _gameOverText.text = "Game Over";
        }

        public void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
