using UnityEngine;

namespace Madhouse.ADHD
{
    public class ShapeMove : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private ShapeInteraction _shapeInteraction;

        private Vector2 _directionMove;
        private Vector3 _downLeftCamera;
        private Vector3 _topRightCamera;
        private float _cooldownCheck = 0.5f;
        private float _timerCheck;

        private void Awake()
        {
            _topRightCamera = Camera.main.ViewportToWorldPoint(Vector3.one);
            _downLeftCamera = Camera.main.ViewportToWorldPoint(Vector3.zero);
            _directionMove = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            _timerCheck = Time.time + _cooldownCheck;
        }

        private void Update()
        {
            if (_shapeInteraction.IsInteraction)
                return;

            if (_timerCheck < Time.time)
                CheckCollision();

            Move();
        }

        private void Move()
        {
            _rigidbody2D.velocity += _directionMove * _speed * Time.deltaTime;
        }

        private void ChangeDirection(bool isChangeX)
        {
            _rigidbody2D.velocity = Vector2.zero;
            if (isChangeX)
                _directionMove.x = -_directionMove.x;
            else
                _directionMove.y = -_directionMove.y;
            _timerCheck = Time.time + _cooldownCheck;
        }

        private void CheckCollision()
        {
            if (transform.position.x < _downLeftCamera.x && _directionMove.x < 0)
            {
                ChangeDirection(true);
            }
            else if(transform.position.x > _topRightCamera.x && _directionMove.x > 0)
            {
                ChangeDirection(true);
            }
            else if (transform.position.y < _downLeftCamera.y && _directionMove.y < 0)
            {
                ChangeDirection(false);
            }
            else if (transform.position.y > _topRightCamera.y && _directionMove.y > 0)
            {
                ChangeDirection(false);
            }
        }
    }

}
