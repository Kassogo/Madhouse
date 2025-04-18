using System;
using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Модель специальной фигуры.
    /// </summary>
    [Serializable]
    public class SpecialShapeModel
    {
        [SerializeField] private SpecialShapeTypes _type;
        [SerializeField] private Sprite _sprite;

        /// <summary>
        /// Тип специальной фигуры.
        /// </summary>
        public SpecialShapeTypes Type => _type;

        /// <summary>
        /// Спрайт фигуры.
        /// </summary>
        public Sprite Picture => _sprite;
    }
}
