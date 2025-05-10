using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Настройки фигуры.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ShapeSetting), menuName = "Madhouse/ADHD/" + nameof(ShapeSetting))]
    public class ShapeSetting : ScriptableObject
    {
        [Header("Movement")]
        [SerializeField] private float _speedMove;
        [SerializeField] private float _cooldownCheckBorders = 0.5f;
        [SerializeField] private float _offsetClashCameraBoard = 0.5f;
        [Space]
        [Header("Interaction")]
        [SerializeField] private LayerMask _layerMaskBackpack;
        [SerializeField] private float _rayDistance = 0.5f;
        [SerializeField] private float _timeForClick = 0.1f;
        [SerializeField] private float _timeForClamped = 0.5f;
        [SerializeField] private float _shakeDistance = 0.2f;
        [SerializeField] private int _countShakeNeed = 6;
        [Header("LifeTime")]
        [SerializeField] private float _minTimeOverShape = 15;
        [SerializeField] private float _maxTimeOverShape = 20;

        /// <summary>
        /// Скорость движения.
        /// </summary>
        public float SpeedMove => _speedMove;

        /// <summary>
        /// Кулдаун проверки границ камеры.
        /// </summary>
        public float CooldownCheckBorders => _cooldownCheckBorders;

        /// <summary>
        /// Расстояние удара фигуры от края карты.
        /// </summary>
        public float OffsetClashCameraBoard => _offsetClashCameraBoard;

        /// <summary>
        /// Слой физики рюкзака.
        /// </summary>
        public LayerMask LayerMaskBackpack => _layerMaskBackpack;

        /// <summary>
        /// Длина луча проверки контакта с рюкзаком.
        /// </summary>
        public float RayDistanceCheck => _rayDistance;

        /// <summary>
        /// Время для клика.
        /// </summary>
        public float TimeForClick => _timeForClick;

        /// <summary>
        /// Время для тряски.
        /// </summary>
        public float TimeForClamped => _timeForClamped;

        /// <summary>
        /// Дистанция при тряски.
        /// </summary>
        public float ShakeDistance => _shakeDistance;

        /// <summary>
        /// Кол-во покачиваний при тряске.
        /// </summary>
        public int CountShakeNeed => _countShakeNeed;

        /// <summary>
        /// Минимальное кол-во времени жизни фигуры.
        /// </summary>
        public float MinTimeOverShape => _minTimeOverShape;

        /// <summary>
        /// Максимальное кол-во времени жизни фигуры.
        /// </summary>
        public float MaxTimeOverShape => _maxTimeOverShape;
    }
}
