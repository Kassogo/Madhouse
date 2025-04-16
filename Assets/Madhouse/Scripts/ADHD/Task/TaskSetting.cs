using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Настройки для системы заданий.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(TaskSetting), menuName = "Madhouse/ADHD/" + nameof(TaskSetting))]
    public class TaskSetting : ScriptableObject
    {
        [SerializeField] private float _minTimeChangeTask = 8;
        [SerializeField] private float _maxTimeChangeTask = 15;
        [Space]
        [SerializeField] private float _minTimeSpecialTask = 20;
        [SerializeField] private float _maxTimeSpecialTask = 30;

        /// <summary>
        /// Минимальное время изменения заданий.
        /// </summary>
        public float MinTimeChangeTask => _minTimeChangeTask;

        /// <summary>
        /// Максимальное время изменения заданий.
        /// </summary>
        public float MaxTimeChangeTask => _maxTimeChangeTask;

        /// <summary>
        /// Минимальное время изменения особых заданий.
        /// </summary>
        public float MinTimeChangeSpecialTask => _minTimeSpecialTask;

        /// <summary>
        /// Максимальное время изменения особых заданий.
        /// </summary>
        public float MaxTimeChangeSpecialTask => _maxTimeSpecialTask;
    }
}
