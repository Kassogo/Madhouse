using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

/// <summary>
/// This script controls the behavior of the Game Over panel in the game.
/// </summary >
namespace Madhouse.BipolarDisorder
{
    public class GameOverPanel : MonoBehaviour
    {
        public static GameOverPanel Instance { get; private set; }
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TextMeshProUGUI _gameOverText;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            _gameOverPanel.SetActive(false); // —крываем панель при старте
        }

        /// <summary>
        /// Method to restart the game by reloading the current scene
        /// </summary >
        public void RestartGame()
        {
            // —брасываем жизни и очки перед перезапуском
            if (LifeManager.Instance != null)
            {
                LifeManager.Instance.ResetLives();
            }
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.ResetScore();
            }
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        private void Start()
        {
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

        public void ShowGameOver()
        {
            if (_gameOverPanel != null)
            {
                _gameOverPanel.SetActive(true);
                if (_gameOverText != null)
                {
                    _gameOverText.text = "Game Over";
                }
                else
                {
                    Debug.LogError("GameOverPanel: _gameOverText is null!");
                }
            }
            else
            {
                Debug.LogError("GameOverPanel: _gameOverPanel is null!");
            }
        }
    }
}