using System;
using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Модель задачи.
    /// </summary>
    [Serializable]
    public class Task
    {
        [SerializeField] private TaskType _taskType;
        [SerializeField] private InteractionEndTypes _interactionEnd;
        [SerializeField] private ShapeColors _shapeColor;
        [SerializeField] private ShapeTypes _shapeForm;

        /// <summary>
        /// Тип задачи.
        /// </summary>
        public TaskType Type => _taskType;

        /// <summary>
        /// Тип итерации игрока с фигурой.
        /// </summary>
        public InteractionEndTypes Interaction => _interactionEnd;

        /// <summary>
        /// Цвет фигуры.
        /// </summary>
        public ShapeColors ShapeColor => _shapeColor;

        /// <summary>
        /// Тип фигуры.
        /// </summary>
        public ShapeTypes Form => _shapeForm;
    }
}