using UnityEngine;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// Spawns thoughts at a defined spawn point.
    /// The spawn rate increases as the player's score increases.
    /// </summary>
    public class ThoughtsSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _thoughtPrefab;
        [SerializeField] private Transform _spawnPoint;
        private float _spawnInterval = 1.5f;
        private const float _minSpawnInterval = 0.5f; // ћинимальный интервал между спавнами
        private const float _decreaseFactor = 0.05f; // Ќасколько быстрее будут по€вл€тьс€ мысли за 1 очко

        private void Start()
        {
            InvokeRepeating(nameof(SpawnThought), 1f, _spawnInterval);
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnScoreChanged += AdjustSpawnRate;
            }
        }

        private void OnDestroy()
        {
            if (ScoreManager.Instance != null)
            {
                ScoreManager.Instance.OnScoreChanged -= AdjustSpawnRate;
            }
        }

        private void SpawnThought()
        {
            if (_thoughtPrefab == null || _spawnPoint == null) return;

            GameObject newThought = Instantiate(_thoughtPrefab, _spawnPoint.position, Quaternion.identity);
            AssignColor(newThought);
        }

        private void AssignColor(GameObject thought)
        {
            ThoughtsColor thoughtsColor = thought.GetComponent<ThoughtsColor>();
            if (thoughtsColor != null)
            {
                thoughtsColor.SetRandomColor();
            }
        }

        private void AdjustSpawnRate(int score)
        {
            float newInterval = Mathf.Max(_minSpawnInterval, 1.5f - score * _decreaseFactor);
            if (Mathf.Abs(newInterval - _spawnInterval) > 0.01f)
            {
                _spawnInterval = newInterval;
                CancelInvoke(nameof(SpawnThought));
                InvokeRepeating(nameof(SpawnThought), _spawnInterval, _spawnInterval);
            }
        }
    }
}
