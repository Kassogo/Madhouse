using UnityEngine;

namespace Madhouse.AnxietyDisorder
{
    public class EnemiesSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _enemy;

        [SerializeField] private Transform _point1;
        [SerializeField] private Transform _point2;
        [SerializeField] private Transform _point3;
        [SerializeField] private Transform _point4;

        [SerializeField] private float _enemySpawnTimer;
        private float _enemySpawnTimerCurrent;

        void Start()
        {
            _enemySpawnTimerCurrent = _enemySpawnTimer;
        }

        void Update()
        {
            if (_enemySpawnTimerCurrent <= 0)
            {
                Instantiate(_enemy, SetEnemyPosition(), Quaternion.identity);
                _enemySpawnTimer = Random.Range(3, 10);
                _enemySpawnTimerCurrent = _enemySpawnTimer;
            }
            else
            {
                _enemySpawnTimerCurrent -= Time.deltaTime;
            }
        }

        private Vector3 SetEnemyPosition()
        {
            int random = Random.Range(1, 5);
            Vector3 vector = new Vector3();
            switch (random)
            {
                case 1: vector = new Vector3((Random.Range(_point1.position.x, _point2.position.x)), _point1.position.y, 0); break;
                case 2: vector = new Vector3((Random.Range(_point1.position.x, _point2.position.x)), _point4.position.y, 0); break;
                case 3: vector = new Vector3(_point1.position.x, Random.Range(_point4.position.y, _point1.position.y), 0); break;
                case 4: vector = new Vector3(_point2.position.x, Random.Range(_point4.position.y, _point1.position.y), 0); break;
            }
            return vector;
        }
    }

}