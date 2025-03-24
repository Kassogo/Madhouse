using UnityEngine;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// Spawns thoughts at a defined spawn point.
    /// </summary>
    public class ThoughtsSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _thoughtPrefab;
        [SerializeField] private Transform _spawnPoint;
        
        private float spawnInterval = 1.5f;

        private void Start()
        {
            InvokeRepeating(nameof(SpawnThought), 1f, spawnInterval);
        }

        private void SpawnThought()
        {
            if (_thoughtPrefab == null || _spawnPoint == null) return;

            GameObject newThought = Instantiate(_thoughtPrefab, _spawnPoint.position, Quaternion.identity);
            AssignColor(newThought);
        }

        private void AssignColor(GameObject thought)
        {
            if (thought.TryGetComponent(out ThoughtsColor thoughtsColor))
            {
                thoughtsColor.SetRandomColor();
            }
        }
    }
}
