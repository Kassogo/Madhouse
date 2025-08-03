using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Madhouse.BipolarDisorder
{
    public class GameOverPanel : MonoBehaviour
    {
        public static GameOverPanel Instance { get; private set; }
        [SerializeField] private GameObject _gameOverPanel;
        [SerializeField] private TextMeshProUGUI _gameOverText;
        [SerializeField] private GameObject _restartButton;
        [SerializeField] private GameObject _exitButton;

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

            _gameOverPanel.SetActive(false);
        }

        public void RestartGame()
        {
            Time.timeScale = 1;
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

        public void QuitGame()
        {
            SceneManager.LoadScene(0);
        }

        private void Start()
        {
            if (LifeManager.Instance != null)
            {
                LifeManager.Instance.OnLivesChanged += CheckGameOver;
            }
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnGameWin += ShowWin;
            }
        }

        private void OnDestroy()
        {
            if (LifeManager.Instance != null)
            {
                LifeManager.Instance.OnLivesChanged -= CheckGameOver;
            }
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnGameWin -= ShowWin;
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
                if (_restartButton != null) _restartButton.SetActive(true);
                if (_exitButton != null) _exitButton.SetActive(false);
                Time.timeScale = 0;
            }
        }

        public void ShowWin()
        {
            if (_gameOverPanel != null)
            {
                _gameOverPanel.SetActive(true);
                if (_gameOverText != null)
                {
                    _gameOverText.text = "Win";
                }
                if (_restartButton != null) _restartButton.SetActive(false);
                if (_exitButton != null) _exitButton.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}