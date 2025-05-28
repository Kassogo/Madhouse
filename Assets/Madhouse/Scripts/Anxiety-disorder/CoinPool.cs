using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Madhouse.AnxietyDisorder
{
    public class CoinPool : MonoBehaviour
    {
        public static CoinPool instance;

        [SerializeField] private int poolCount = 3;
        [SerializeField] private bool autoExpand = false;
        [SerializeField] private Coin coinPrefab;

        private CoinPoolMono<Coin> pool;

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            this.pool = new CoinPoolMono<Coin>(this.coinPrefab, this.poolCount, this.transform);
            this.pool.autoExpand = this.autoExpand;
        }

        public void CreateCoin(Vector3 vector, float size)
        {
            var coin = this.pool.GetFreeElement();
            coin.transform.position = vector;
            coin.transform.localScale = new Vector3(size, size, size);
        }
    }
}
