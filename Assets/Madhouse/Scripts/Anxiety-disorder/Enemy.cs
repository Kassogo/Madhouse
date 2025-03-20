using System.Collections;
using System.Threading;
using UnityEngine;

namespace Madhouse.AnxietyDisorder
{
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed;
        private Transform _target;
        private bool _canMove;
        private bool _canAttack;

        private void Start()
        {
            _target = GameObject.FindGameObjectWithTag("Brain").GetComponent<Transform>();
            _canMove = true;
            _canAttack = true;
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
    }
}
