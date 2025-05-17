using System.Collections;
using UnityEngine;

//In this script, enemies head towards the brain, move, take damage, are destroyed, and attack the brain
namespace Madhouse.AnxietyDisorder
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        [SerializeField] private GameObject _damageVisualGO;
        private float _healthPoit;
        private Transform _target;
        private bool _canMove;
        private bool _canAttack;
        private float _reloadTimer = 1f;

        private void OnEnable()
        {
            _target = GameObject.FindGameObjectWithTag("Brain").transform;
            _canMove = true;
            _canAttack = true;
            _healthPoit = 5;
            _damageVisualGO.SetActive(false);
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
                transform.position = Vector2.MoveTowards(transform.position, _target.transform.position, _moveSpeed * Time.deltaTime);
            }
        }

        //taking away the enemy's health and destroying the enemy
        public void TakeEnemyDamage()
        {
            _healthPoit -= 1;
            if (_healthPoit <= 0)
            {
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
            if (collision.gameObject.CompareTag("Brain"))
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
                    HealthBar.instance.Damage(1f);
                    StartCoroutine(ReloadAttack());
                }
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
            TakeEnemyDamage();
        }

        //animation of hitting an enemy
        private IEnumerator DamageVisual()
        {
            _damageVisualGO.SetActive(true);
            yield return new WaitForSeconds(0.1f);
            _damageVisualGO.SetActive(false);
        }
    }
}
