using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Madhouse.BipolarDisorder
{
    public class ThoughtsSpawner : MonoBehaviour
    {
        public GameObject ThoughtPrefab; 
        public float spawnInterval = 1.5f; 
        public float spawnRangeX = 8f;
        void Start()
        {
            InvokeRepeating("SpawnThought", 1f, spawnInterval);
        }


        void SpawnThought()
        {
            if (ThoughtPrefab == null) return;

            Vector2 spawnPosition = new Vector2(0, 7);

            GameObject newThought = Instantiate(ThoughtPrefab, spawnPosition, Quaternion.identity);
            
            SpriteRenderer sr = newThought.GetComponent<SpriteRenderer>();
            sr.color = Random.value > 0.5f ? Color.white : Color.black;
        }
    }
}
