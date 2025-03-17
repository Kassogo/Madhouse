using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Madhouse.ADHD
{
    public class BackpackMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 10f;

        private bool _isMoveRight = true;
        private Vector3 _leftPoint;
        private Vector3 _rightPoint;
        private Vector3 _newPosition;
        private float _minChangeDistance = 0.001f;
        private float _offset = 1f;

        private void Awake()
        {
            _leftPoint = Camera.main.ViewportToWorldPoint(Vector2.zero);
            _rightPoint = Camera.main.ViewportToWorldPoint(new Vector2(1, 0));

            _leftPoint.x += _offset;
            _leftPoint.y += _offset;
            _leftPoint.z = transform.position.z;

            _rightPoint.x -= _offset;
            _rightPoint.y += _offset;
            _rightPoint.z = transform.position.z;
        }

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            _newPosition = Vector3.Lerp(transform.position, _isMoveRight ? _rightPoint : _leftPoint, _speed * Time.deltaTime);

            if(Mathf.Abs(_newPosition.x - transform.position.x) < _minChangeDistance)
                _isMoveRight = !_isMoveRight;
            
            transform.position = _newPosition;
        }
    }
}
