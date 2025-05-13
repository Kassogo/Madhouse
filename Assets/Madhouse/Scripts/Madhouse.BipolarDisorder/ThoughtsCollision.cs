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
        [SerializeField] private ThoughtsSpawner _thoughtsSpawner; // —сылка на спаунер
        [SerializeField] private ColorController _colorController; // —сылка на контроллер цвета игрока

        private void OnEnable()
        {
            if (_thoughtsSpawner != null)
            {
                _thoughtsSpawner.OnThoughtSpawnedColor.AddListener(OnThoughtSpawned);
            }
            else
            {
                Debug.LogError("ThoughtsCollision: ThoughtsSpawner не назначен!");
            }
        }

        private void OnDisable()
        {
            if (_thoughtsSpawner != null)
            {
                _thoughtsSpawner.OnThoughtSpawnedColor.RemoveListener(OnThoughtSpawned);
            }
        }

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

        private void OnThoughtSpawned(Color spawnedColor)
        {
         
        }
    }
}