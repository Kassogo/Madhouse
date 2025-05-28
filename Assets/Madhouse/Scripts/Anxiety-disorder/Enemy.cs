using System.Collections;
using UnityEngine;

//In this script, enemies head towards the brain, move, take damage, are destroyed, and attack the brain
namespace Madhouse.AnxietyDisorder
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;

        [SerializeField] private GameObject _enemyVisual1;
        [SerializeField] private GameObject _enemyVisual2;
        [SerializeField] private GameObject _enemyVisual3;
        [SerializeField] private GameObject _enemyDamageVisual1;
        [SerializeField] private GameObject _enemyDamageVisual2;
        [SerializeField] private GameObject _enemyDamageVisual3;

        private float _direction;
        private float _healthPoit;
        private Transform _target;
        private bool _canMove;
        private bool _canAttack;
        private float _reloadTimer = 1f;
        private float _randomVisual;

        private void OnEnable()
        {
            _target = GameObject.FindGameObjectWithTag("BrainCentre").transform;
            _canMove = true;
            _canAttack = true;
            _healthPoit = 8;

            _direction = 1;

            _enemyVisual1.SetActive(false);
            _enemyVisual2.SetActive(false);
            _enemyVisual3.SetActive(false);
            _enemyDamageVisual1.SetActive(false);
            _enemyDamageVisual2.SetActive(false);
            _enemyDamageVisual3.SetActive(false);
            _randomVisual = Random.Range(1, 4);

            if (_randomVisual == 1)
            {
                _enemyVisual1.SetActive(true);
            }
            else if (_randomVisual == 2)
            {
                _enemyVisual2.SetActive(true);
            }
            else
            {
                _enemyVisual3.SetActive(true);
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
            _healthPoit -= 1;

            if (_healthPoit <= 0)
            {
                CoinPool.instance.CreateCoin(transform.position, 1f);
                Money.instance.AddMoney(5);
                ExplosionPool.instance.CreateExplosion(transform.position, 0.1f);
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
                    HealthBar.instance.Damage(3f);
                    StartCoroutine(ReloadAttack());
                }
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Brain"))
            {
                _canMove = true;
            }
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
            if(Bonuses.instance.mindBreathOn == false)
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
            else
            {
                _enemyDamageVisual3.SetActive(true);
                yield return new WaitForSeconds(0.1f);
                _enemyDamageVisual3.SetActive(false);
            }
        }

        private void Flip()
        {
            if (transform.position.x < 0)
            {
                _enemyVisual1.GetComponent<SpriteRenderer>().flipX = true;
                _enemyVisual2.GetComponent<SpriteRenderer>().flipX = true;
                _enemyVisual3.GetComponent<SpriteRenderer>().flipX = true;
                _enemyDamageVisual1.GetComponent<SpriteRenderer>().flipX = true;
                _enemyDamageVisual2.GetComponent<SpriteRenderer>().flipX = true;
                _enemyDamageVisual3.GetComponent<SpriteRenderer>().flipX = true;
            }
            else
            {
                _enemyVisual1.GetComponent<SpriteRenderer>().flipX = false;
                _enemyVisual2.GetComponent<SpriteRenderer>().flipX = false;
                _enemyVisual3.GetComponent<SpriteRenderer>().flipX = false;
                _enemyDamageVisual1.GetComponent<SpriteRenderer>().flipX = false;
                _enemyDamageVisual2.GetComponent<SpriteRenderer>().flipX = false;
                _enemyDamageVisual3.GetComponent<SpriteRenderer>().flipX = false;
            }
        }
    }
}