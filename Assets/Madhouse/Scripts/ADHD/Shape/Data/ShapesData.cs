using System.Collections.Generic;
using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Модели фигур.
    /// </summary>
    [CreateAssetMenu(fileName ="ShapesData", menuName = "Madhouse/ADHD/ShapesData")]
    public class ShapesData : ScriptableObject
    {
        [SerializeField] private List<ShapeModel> _shapeModels;

        /// <summary>
        /// Модели фигур.
        /// </summary>
        public List<ShapeModel> Shapes => _shapeModels;
    }
}
