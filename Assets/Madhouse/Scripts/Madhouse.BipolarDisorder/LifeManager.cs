using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// Manages player lives.
    /// </summary>
    public class LifeManager : MonoBehaviour
    {
        public static LifeManager Instance { get; private set; }
        public int Lives => _lives;

        [SerializeField] private int _startLives = 3;
        private int _lives;
        public event Action<int> OnLivesChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                // DontDestroyOnLoad(gameObject); // ���������, ��� ���� ������ ��� ��� ��� ����������������
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            _lives = _startLives;
            OnLivesChanged?.Invoke(Lives);
        }

        private void OnEnable()
        {
            // ������������� ���������� ����� ��� ��������� ������� (����� ������������ �����)
            _lives = _startLives;
            OnLivesChanged?.Invoke(Lives);
        }

        public void LoseLife()
        {
            _lives--;
            OnLivesChanged?.Invoke(Lives);

            if (Lives <= 0)
            {
                GameOver();
            }
        }

        private void GameOver()
        {
            FindObjectOfType<BackgroundManager>().OnLifeLost();
            // ���������� ������ Game Over
            if (GameOverPanel.Instance != null)
            {
                GameOverPanel.Instance.ShowGameOver();
            }
            else
            {
                Debug.LogError("GameOverPanel.Instance is null!");
                // ����� �������� �������� ����� Game Over, ���� ��� ���������
            }
        }

        public void ResetLives() // ��������� ����� ��� ������ ������ ������
        {
            _lives = _startLives;
            OnLivesChanged?.Invoke(Lives);
        }
    }
}