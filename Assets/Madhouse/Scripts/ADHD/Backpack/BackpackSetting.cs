using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Настройка рукзака
    /// </summary>
    [CreateAssetMenu(fileName = nameof(BackpackSetting), menuName = "Madhouse/ADHD/" + nameof(BackpackSetting))]
    public class BackpackSetting : ScriptableObject
    {
        [SerializeField] private float _speed = 10f;
        [SerializeField] private float _offsetCameraAngles = 1f;

        /// <summary>
        /// Скорость движения.
        /// </summary>
        public float Speed => _speed;

        /// <summary>
        /// Отступ от краёв камеры.
        /// </summary>
        public float OffsetCameraAngles => _offsetCameraAngles;
    }
}
