using UnityEngine;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// Manages the player's score.
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        public static ScoreManager Instance { get; private set; }
        public int _score = 0;
        public event System.Action<int> OnScoreChanged;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Теперь объект не уничтожается при смене сцены
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        }

        public void UpdateScore(bool isMatch)
        {
            _score += isMatch ? 1 : -1;
            Debug.Log($"UpdateScore: {_score}");
            OnScoreChanged?.Invoke(_score);

#if UNITY_EDITOR
            Debug.ClearDeveloperConsole();
            Debug.Log($"Score: {_score}");
#endif
        }
    }
}
