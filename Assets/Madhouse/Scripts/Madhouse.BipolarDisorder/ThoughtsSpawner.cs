using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// This method spawns thoughts
    /// </summary>
    public class ThoughtsSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _thoughtPrefab;
        [SerializeField] private Transform _spawnPoint;

        private float spawnInterval = 1.5f;
        
        void Start()
        {
            InvokeRepeating("SpawnThought", 1f, spawnInterval);
        }

        void SpawnThought()
        {
            if (_thoughtPrefab == null || _spawnPoint == null) return;

            GameObject newThought = Instantiate(_thoughtPrefab, _spawnPoint.position, Quaternion.identity);

            SpriteRenderer spriteRenderer = newThought.GetComponent<SpriteRenderer>();
            spriteRenderer.color = Random.value > 0.5f ? Color.white : Color.black;
        }
    }
}
