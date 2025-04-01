using Madhouse.AnxietyDisorder;
using UnityEngine;

//I copied this code...
public class EnemyPool : MonoBehaviour
{
    [SerializeField] private int poolCount = 3;
    [SerializeField] private bool autoExpand = false;
    [SerializeField] private Enemy enemyPrefab;

    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;
    [SerializeField] private Transform _point3;

    [SerializeField] private float _enemySpawnTimer;
    private float _enemySpawnTimerCurrent;

    private int _minTimerScale = 1;
    private int _maxTimerScale = 4;

    private PoolMono<Enemy> pool;

    private void Start()
    {
        this.pool = new PoolMono<Enemy>(this.enemyPrefab, this.poolCount, this.transform);
        this.pool.autoExpand = this.autoExpand;
    }

    private void Update()
    {
        if (_enemySpawnTimerCurrent <= 0)
        {
            CreateEnemy();
            ReloadTimer();
        }
        else
        {
            _enemySpawnTimerCurrent -= Time.deltaTime;
        }
    }

    private void ReloadTimer()
    {
        _enemySpawnTimer = Random.Range(_minTimerScale, _maxTimerScale);
        _enemySpawnTimerCurrent = _enemySpawnTimer;
    }

    private void CreateEnemy()
    {
        var enemy = this.pool.GetFreeElement();
        enemy.transform.position = CalcEnemyPosition();
    }

    private Vector3 CalcEnemyPosition()
    {
        int random = Random.Range(1, 5);
        Vector3 vector = new Vector3();
        switch (random)
        {
            case 1: vector = new Vector3((Random.Range(_point1.position.x, _point2.position.x)), _point1.position.y, 0); break;
            case 2: vector = new Vector3((Random.Range(_point1.position.x, _point2.position.x)), _point3.position.y, 0); break;
            case 3: vector = new Vector3(_point1.position.x, Random.Range(_point3.position.y, _point1.position.y), 0); break;
            case 4: vector = new Vector3(_point2.position.x, Random.Range(_point3.position.y, _point1.position.y), 0); break;
        }
        return vector;
    }
}
