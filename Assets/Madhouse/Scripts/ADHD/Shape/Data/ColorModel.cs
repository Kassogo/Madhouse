using System;
using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Модель цвета для фигур.
    /// </summary>
    [Serializable]
    public class ColorModel
    {
        [SerializeField] private ShapeColors _shapeColor;
        [SerializeField] private Color _color;

        /// <summary>
        /// Тип цвета.
        /// </summary>
        public ShapeColors ColorType => _shapeColor;

        /// <summary>
        /// Значение цвета для спрайта.
        /// </summary>
        public Color ColorValue => _color;
    }
}