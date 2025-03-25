using System.Collections;
using System.Threading;
using UnityEngine;

namespace Madhouse.AnxietyDisorder
{
    public class Enemy : MonoBehaviour
    {
        public static Enemy instance;

        [SerializeField] private float _moveSpeed;
        [SerializeField] private GameObject _damageVisualGO;
        private float _healthPoit;
        private Transform _target;
        private bool _canMove;
        private bool _canAttack;

        public void TakeEnemyDamage()
        {
            _healthPoit -= 1;
            if(_healthPoit <= 0)
            {
                Destroy(this.gameObject);
            }
        }

        private void Awake()
        {
            instance = this;
        }
        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Brain").GetComponent<Transform>();
            _canMove = true;
            _canAttack = true;
            _healthPoit = 5;
            _damageVisualGO.SetActive(false);
        }
        private void FixedUpdate()
        {
            if (_canMove == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _moveSpeed * Time.fixedDeltaTime);
            }
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Brain"))
            {
                _canMove = false;
            }
        }
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Brain"))
            {
                if (_canAttack)
                {
                    HealthBar.instance.Damage(1f);
                    StartCoroutine(_reloadAttack());
                }

            }
        }
        IEnumerator _reloadAttack()
        {
            _canAttack = false;
            yield return new WaitForSeconds(1f);
            _canAttack = true;
        }

        private void OnMouseDown()
        {
            TakeEnemyDamage();
            StartCoroutine(_damageVisual());
        }
        IEnumerator _damageVisual()
        {
            _damageVisualGO.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _damageVisualGO.SetActive(false);
        }
    }
}
