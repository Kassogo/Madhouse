using UnityEngine;
using System;
using UnityEngine.SceneManagement;

namespace Madhouse.BipolarDisorder
{
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }
        public int Score => _score;

        private int _score = 0;
        private const int _winScore = 100;
        public event Action<int> OnScoreChanged;
        public event Action OnGameWin;

        private void Awake()
        {
            InitializeSingleton();
        }

        private void InitializeSingleton()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            _score = 0;
            OnScoreChanged?.Invoke(_score);
        }

        public void UpdateScore(bool isMatch)
        {
            if (Score >= _winScore) return;

            _score += isMatch ? 1 : -1;
            OnScoreChanged?.Invoke(_score);

            FindObjectOfType<BackgroundManager>()?.UpdateScore(_score);

            if (_score >= _winScore)
            {
                OnGameWin?.Invoke();
            }
        }

        public void ResetScore()
        {
            _score = 0;
            OnScoreChanged?.Invoke(_score);
        }
    }
}