using UnityEngine;
using UnityEngine.Events;

namespace Madhouse.BipolarDisorder
{
    public class ThoughtsSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _thoughtPrefab;
        [SerializeField] private Transform _spawnPoint;
        private float _spawnInterval = 1.5f;
        private const float _minSpawnInterval = 0.5f;
        private const float _decreaseFactor = 0.05f;

        // Объявляем событие, которое передает цвет
        public UnityEvent<Color> OnThoughtSpawnedColor = new UnityEvent<Color>();

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
            Color spawnedColor = AssignColor(newThought);

            // Вызываем событие и передаем цвет
            OnThoughtSpawnedColor?.Invoke(spawnedColor);
        }

        private Color AssignColor(GameObject thought)
        {
            ThoughtsColor thoughtsColor = thought.GetComponent<ThoughtsColor>();
            Color colorNow = Random.Range(0, 2) == 0 ? Color.black : Color.white;

            if (thoughtsColor != null)
            {
                thoughtsColor.SetColor(colorNow);
            }
            return colorNow;
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