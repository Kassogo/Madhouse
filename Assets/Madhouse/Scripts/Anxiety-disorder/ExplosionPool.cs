using Madhouse.AnxietyDisorder;
using UnityEngine;

namespace Madhouse.AnxietyDisorder
{
    public class ExplosionPool : MonoBehaviour
    {
        public static ExplosionPool instance;

        [SerializeField] private int poolCount = 2;
        [SerializeField] private bool autoExpand = false;
        [SerializeField] private Explosion explosionPrefab;

        private PoolMono<Explosion> pool;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            this.pool = new PoolMono<Explosion>(this.explosionPrefab, this.poolCount, this.transform);
            this.pool.autoExpand = this.autoExpand;
        }

        public void CreateExplosion(Vector3 vector, float size)
        {
            var explosion = this.pool.GetFreeElement();
            explosion.transform.position = vector;
            explosion.transform.localScale = new Vector3(size, size, size);
        }
    }
}