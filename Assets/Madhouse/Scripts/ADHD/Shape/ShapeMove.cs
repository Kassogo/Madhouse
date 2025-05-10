using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Класс-компонент, отвечающий за движение фигуры
    /// </summary>
    public class ShapeMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private ShapeInteraction _shapeInteraction;

        private Vector2 _directionMove;
        private Vector3 _downLeftCamera;
        private Vector3 _topRightCamera;
        private float _timerCheck;

        private ShapeSetting _setting;

        public void Init(ShapeSetting setting, ShapeInteraction shapeInteraction)
        {
            _shapeInteraction = shapeInteraction;
            _setting = setting;
            _topRightCamera = Camera.main.ViewportToWorldPoint(Vector3.one) - Vector3.one * setting.OffsetClashCameraBoard;
            _downLeftCamera = Camera.main.ViewportToWorldPoint(Vector3.zero) + Vector3.one * setting.OffsetClashCameraBoard;
            _directionMove = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
            _timerCheck = Time.time + _setting.CooldownCheckBorders;
        }


        private void Update()
        {
            if (_shapeInteraction.IsInteraction || _setting == null)
                return;

            if (_timerCheck < Time.time)
                CheckCollision();

            Move();
        }

        private void Move()
        {
            _rigidbody2D.velocity += _directionMove * _setting.SpeedMove * Time.deltaTime;
        }

        private void ChangeDirection(bool isChangeX)
        {
            _rigidbody2D.velocity = Vector2.zero;
            if (isChangeX)
                _directionMove.x = -_directionMove.x;
            else
                _directionMove.y = -_directionMove.y;
            _timerCheck = Time.time + _setting.CooldownCheckBorders;
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
