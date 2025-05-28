using System.Collections;
using UnityEngine;

//In this script, enemies head towards the brain, move, take damage, are destroyed, and attack the brain
namespace Madhouse.AnxietyDisorder
{
    public class BigEnemy : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;

        [SerializeField] private GameObject _enemyVisual1;
        [SerializeField] private GameObject _enemyVisual2;
        [SerializeField] private GameObject _enemyDamageVisual1;
        [SerializeField] private GameObject _enemyDamageVisual2;

        private float _direction;
        private float _healthPoint;
        private Transform _target;
        private bool _canMove;
        private bool _canAttack;
        private float _reloadTimer = 1f;
        private float _randomVisual;

        private void OnEnable()
        {
            _target = GameObject.FindGameObjectWithTag("BrainCentre").transform;
            _direction = 1;
            _canMove = true;
            _canAttack = true;
            _healthPoint = 16;

            _enemyVisual1.SetActive(false);
            _enemyVisual2.SetActive(false);
            _enemyDamageVisual1.SetActive(false);
            _enemyDamageVisual2.SetActive(false);

            _randomVisual = Random.Range(1, 3);
            if (_randomVisual == 1)
            {
                _enemyVisual1.SetActive(true);
            }
            else
            {
                _enemyVisual2.SetActive(true);
            }
        }

        private void OnDisable()
        {
            this.StopCoroutine("_reloadAttack");
            this.StopCoroutine("_damageVisual");
        }

        private void FixedUpdate()
        {
            if (_canMove == true)
            {
                transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _moveSpeed * Time.deltaTime * _direction * Bonuses.instance.mentalOrderScale);
            }
            Flip();
        }

        //taking away the enemy's health and destroying the enemy
        public void TakeEnemyDamage()
        {
            _healthPoint -= 1;

            if (_healthPoint <= 0)
            {
                CoinPool.instance.CreateCoin(transform.position, 1.5f);
                Money.instance.AddMoney(10);
                ExplosionPool.instance.CreateExplosion(transform.position, 0.15f);
                this.Deactivate();
            }
            else
            {
                StartCoroutine(DamageVisual());
            }
        }

        //enemy deactivation
        private void Deactivate()
        {
            this.gameObject.SetActive(false);
        }

        //stopping the enemy in a collision with the brain
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Shield"))
            {
                _canMove = true;
                _direction = -1;
            }
            else if (collision.gameObject.CompareTag("Brain"))
            {
                _canMove = false;
            }
        }

        //attacking the enemy during a prolonged collision with the brain
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Brain"))
            {
                if (_canAttack)
                {
                    HealthBar.instance.Damage(9f);
                    StartCoroutine(ReloadAttack());
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Shield"))
            {
                _direction = 1;
            }
        }

        //reloading the attack
        private IEnumerator ReloadAttack()
        {
            _canAttack = false;
            yield return new WaitForSeconds(_reloadTimer);
            _canAttack = true;
        }

        //click on the enemy
        private void OnMouseDown()
        {
            if (Bonuses.instance.mindBreathOn == false)
            {
                TakeEnemyDamage();
            }
        }

        private void OnMouseEnter()
        {
            if (Bonuses.instance.mindBreathOn == true)
            {
                TakeEnemyDamage();
            }
        }

        //animation of hitting an enemy
        private IEnumerator DamageVisual()
        {
            if (_randomVisual == 1)
            {
                _enemyDamageVisual1.SetActive(true);
                yield return new WaitForSeconds(0.1f);
                _enemyDamageVisual1.SetActive(false);
            }
            else if (_randomVisual == 2)
            {
                _enemyDamageVisual2.SetActive(true);
                yield return new WaitForSeconds(0.1f);
                _enemyDamageVisual2.SetActive(false);
            }
        }

        private void Flip()
        {
            if (transform.position.x < 0)
            {
                _enemyVisual1.GetComponent<SpriteRenderer>().flipX = true;
                _enemyVisual2.GetComponent<SpriteRenderer>().flipX = true;
                _enemyDamageVisual1.GetComponent<SpriteRenderer>().flipX = true;
                _enemyDamageVisual2.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                _enemyVisual1.GetComponent<SpriteRenderer>().flipX = false;
                _enemyVisual2.GetComponent<SpriteRenderer>().flipX = false;
                _enemyDamageVisual1.GetComponent<SpriteRenderer>().flipX = false;
                _enemyDamageVisual2.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
}