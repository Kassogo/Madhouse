using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Настройки для диспктчера настроек.
    /// </summary>
    [CreateAssetMenu(fileName = nameof(ShapesDispatcherSetting), menuName = "Madhouse/ADHD/" + nameof(ShapesDispatcherSetting))]
    public class ShapesDispatcherSetting : ScriptableObject
    {
        [SerializeField] private int _startCountShapes = 5;
        [SerializeField] private int _maxCountShapes = 10;
        [SerializeField] private float _cooldownCreatedShape = 4f;

        /// <summary>
        /// Начальное кол-во фигур.
        /// </summary>
        public int StartCountShapes => _startCountShapes;

        /// <summary>
        /// Максимальное кол-во фигур.
        /// </summary>
        public int MaxCountShapes => _maxCountShapes;

        /// <summary>
        /// Время восстановления после создания фигуры.
        /// </summary>
        public float CooldownCreatedShape => _cooldownCreatedShape;
    }
}