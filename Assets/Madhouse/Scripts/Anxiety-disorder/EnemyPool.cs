using Madhouse.AnxietyDisorder;
using UnityEngine;

namespace Madhouse.AnxietyDisorder
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private int poolCount = 3;
        [SerializeField] private bool autoExpand = false;
        [SerializeField] private Enemy enemyPrefab;

        [SerializeField] private Transform _point1;
        [SerializeField] private Transform _point2;
        [SerializeField] private Transform _point3;

        private PoolMono<Enemy> pool;

        private float _timeUnit;
        private float _timer;

        private void Start()
        {
            this.pool = new PoolMono<Enemy>(this.enemyPrefab, this.poolCount, this.transform);
            this.pool.autoExpand = this.autoExpand;

            ReloadTimer();
        }

        private void Update()
        {
            if (_timer > 0)
            {
                _timer -= Time.deltaTime;
            }
            else
            {
                CreateEnemy();
                ReloadTimer();
            }
        }

        private void CreateEnemy()
        {
            var enemy = this.pool.GetFreeElement();
            enemy.transform.position = CalcEnemyPosition();
        }

        private void ReloadTimer()
        {
            _timeUnit = Timer.instance._timeUnit;
            _timer = Random.Range(_timeUnit, _timeUnit * 2f);
        }

        private Vector3 CalcEnemyPosition()
        {
            int random = Random.Range(1, 4);
            Vector3 vector = new Vector3();
            switch (random)
            {
                case 1: vector = new Vector3((Random.Range(_point1.position.x, _point2.position.x)), _point1.position.y, 0); break;
                case 2: vector = new Vector3(_point1.position.x, Random.Range(_point3.position.y, _point1.position.y), 0); break;
                case 3: vector = new Vector3(_point2.position.x, Random.Range(_point3.position.y, _point1.position.y), 0); break;
            }
            return vector;
        }
    }
}