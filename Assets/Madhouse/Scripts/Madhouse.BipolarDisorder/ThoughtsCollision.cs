using UnityEngine;
using UnityEngine.SceneManagement;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// Handles collision detection and delegates score updates.
    /// </summary>
    public class ThoughtsCollision : MonoBehaviour
    {
        [SerializeField] private ColorMatcher _colorMatcher;
        [SerializeField] private ScoreManager _scoreManager;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (!collision.gameObject.TryGetComponent(out SpriteRenderer thoughtRenderer)) return;

            bool isMatch = _colorMatcher.IsColorMatch(thoughtRenderer);
            _scoreManager.UpdateScore(isMatch);

            if (!isMatch && LifeManager.Instance != null)
            {
                LifeManager.Instance.LoseLife();
            }

            Destroy(collision.gameObject);
        }
    }
}