using UnityEngine;
using System;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// Manages player lives.
    /// </summary>
    public class LifeManager : MonoBehaviour
    {
        public static LifeManager Instance { get; private set; }
        public int Lives => _lives;

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

            OnLivesChanged?.Invoke(Lives);

            if (Lives <= 0)
            {
                GameOver();
            }
        }

        private void GameOver()
        {    
            FindObjectOfType<BackgroundManager>().OnLifeLost();        
        }
    }
}
