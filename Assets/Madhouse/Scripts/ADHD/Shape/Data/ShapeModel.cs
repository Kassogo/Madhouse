using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Модель фигур с типом фигуры и спрайтами.
    /// </summary>
    [System.Serializable]
    public class ShapeModel
    {
        [SerializeField] private Sprite _shapePicture;
        [SerializeField] private Sprite _glarePicture;
        [SerializeField] private ShapeTypes _shapeType;

        /// <summary>
        /// Тип фигуры.
        /// </summary>
        public ShapeTypes Type => _shapeType;

        /// <summary>
        /// Спрайт фигуры.
        /// </summary>
        public Sprite Picture => _shapePicture;

        /// <summary>
        /// Спрайт отражения.
        /// </summary>
        public Sprite Glare => _glarePicture;
    }
}