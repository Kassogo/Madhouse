using UnityEngine;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// Manages the player's score.
    /// </summary>
    public class ScoreManager : MonoBehaviour
    {
        private int _score = 0;

        public void UpdateScore(bool isMatch)
        {
            _score += isMatch ? 1 : -1;

#if UNITY_EDITOR
            Debug.ClearDeveloperConsole();
            Debug.Log($"Score: {_score}");
#endif
        }
    }
}

