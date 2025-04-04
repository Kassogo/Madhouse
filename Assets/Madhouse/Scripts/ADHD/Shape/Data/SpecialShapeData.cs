using System.Collections.Generic;
using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Модели специальных фигур.
    /// </summary>
    [CreateAssetMenu(fileName = "SpecialShapesData", menuName = "Madhouse/ADHD/SpecialShapesData")]
    public class SpecialShapeData : ScriptableObject
    {
        [SerializeField] private List<SpecialShapeModel> _specialShapeModels;

        /// <summary>
        /// Модели специальных фигур.
        /// </summary>
        public List<SpecialShapeModel> SpacialShapes => _specialShapeModels;
    }
}
