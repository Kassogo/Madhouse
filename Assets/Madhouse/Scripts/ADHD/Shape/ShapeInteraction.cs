using System;
using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Класс-компонент, отвечающий за взаимодействие игрока с фигурой.
    /// </summary>
    public class ShapeInteraction : MonoBehaviour
    {
        /// <summary>
        /// Событие конца взаимодействия игрока с фигурой.
        /// </summary>
        public event Action<InteractionEndTypes> OnInteractEnd = delegate { };

        [SerializeField] private LayerMask _layerMaskBackpack;

        private Vector3 _offset;
        private Vector3 _mousePosition;
        private Vector3 _oldPosition;

        private float _rayDistance = 0.5f;
        private float _distanceToCamera;
        private float _timeInteraction;
        private float _timeForClick = 0.1f;
        private float _timeForClamped = 0.5f;
        private float _shakeDistance = 0.2f;
        private int _countShakeNeed = 6;
        private int _countShakeDo;

        private bool _isInteraction;

        /// <summary>
        /// Взаимодействует ли игрок с фигурой сейчас
        /// </summary>
        public bool IsInteraction => _isInteraction;

        private void Awake()
        {
            _distanceToCamera = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        }

        private void OnMouseDown()
        {
            _offset = transform.position - GetMouseWorldPosition();
            _isInteraction = true;
        }

        private void OnMouseUp()
        {
            _isInteraction = false;
        }

        private void OnMouseDrag()
        {
            transform.position = GetMouseWorldPosition() + _offset;
        }

        private void Update()
        {
            CheckInteraction();
        }

        private void CheckInteraction()
        {
            if (_isInteraction)
            {
                _timeInteraction += Time.deltaTime;
                if (CheckShake())
                    _countShakeDo++;
            }
            else if (_timeInteraction != 0)
            {
                CheckEndInteraction();

                _timeInteraction = 0;
                _countShakeDo = 0;
            }

            _oldPosition = transform.position;
        }

        private void CheckEndInteraction()
        {
            if (Physics2D.Raycast(transform.position, Vector2.down, _rayDistance, _layerMaskBackpack))
            {
                OnInteractEnd.Invoke(InteractionEndTypes.Taked);
            }
            else if (_countShakeDo >= _countShakeNeed)
            {
                OnInteractEnd.Invoke(InteractionEndTypes.Shaked);
            }
            else if (_timeInteraction >= _timeForClamped)
            {
                OnInteractEnd.Invoke(InteractionEndTypes.Clamped);
            }
            else if (_timeInteraction >= _timeForClick)
            {
                OnInteractEnd.Invoke(InteractionEndTypes.Popped);
            }
        }

        private Vector3 GetMouseWorldPosition()
        {
            _mousePosition = Input.mousePosition;
            _mousePosition.z = _distanceToCamera;
            return Camera.main.ScreenToWorldPoint(_mousePosition);
        }

        private bool CheckShake()
        {
            return MathF.Abs(MathF.Max(transform.position.x, _oldPosition.x) - MathF.Min(transform.position.x, _oldPosition.x))
                + MathF.Abs(MathF.Max(transform.position.y, _oldPosition.y) - MathF.Min(transform.position.y, _oldPosition.y)) > _shakeDistance;
        }
    }
}
