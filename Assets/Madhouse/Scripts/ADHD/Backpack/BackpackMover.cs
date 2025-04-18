using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Класс движения рюкзака.
    /// </summary>
    public class BackpackMover : MonoBehaviour
    {
        [SerializeField] private BackpackSetting _backpackSettings;

        private bool _isMoveRight = true;
        private Vector3 _leftPoint;
        private Vector3 _rightPoint;
        private Vector3 _newPosition;

        private void Awake() => SetPoints();

        private void Update() => Move();

        private void Move()
        {
            _newPosition = transform.position + (_isMoveRight ? Vector3.right : Vector3.left) * _backpackSettings.Speed * Time.deltaTime;

            if((_newPosition.x > _rightPoint.x && _isMoveRight) ||
                (_newPosition.x < _leftPoint.x && !_isMoveRight))
                _isMoveRight = !_isMoveRight;
            
            transform.position = _newPosition;
        }

        private void SetPoints()
        {
            _leftPoint = Camera.main.ViewportToWorldPoint(Vector2.zero);
            _rightPoint = Camera.main.ViewportToWorldPoint(Vector2.right);

            _leftPoint.x += _backpackSettings.OffsetCameraAngles;
            _leftPoint.y += _backpackSettings.OffsetCameraAngles;
            _leftPoint.z = transform.position.z;

            _rightPoint.x -= _backpackSettings.OffsetCameraAngles;
            _rightPoint.y += _backpackSettings.OffsetCameraAngles;
            _rightPoint.z = transform.position.z;
        }
    }
}
