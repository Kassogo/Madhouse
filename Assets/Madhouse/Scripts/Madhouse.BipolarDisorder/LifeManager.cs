using UnityEngine;
using System;
using System.Text.RegularExpressions;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// Manages player lives.
    /// </summary>
    public class LifeManager : MonoBehaviour
    {
        public static LifeManager Instance { get; private set; }

        private int _lives = 3;
        public event Action<int> OnLivesChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        public void LoseLife()
        {
            _lives--;
            
            Debug.Log($"Lives left: {_lives}");
            OnLivesChanged?.Invoke(_lives);

            if (_lives <= 0)
            {
                GameOver();
            }
        }

        public int GetLives() => _lives;

        private void GameOver()
        {
            Debug.Log("Game Over!");
            // Здесь можно добавить логику перезапуска уровня или выхода в главное меню
        }
    }
}
