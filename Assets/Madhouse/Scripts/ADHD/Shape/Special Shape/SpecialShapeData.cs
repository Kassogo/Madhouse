using System.Collections.Generic;
using UnityEngine;

namespace Madhouse.ADHD
{
    [CreateAssetMenu(fileName = "SpecialShapesData", menuName = "Madhouse/ADHD/SpecialShapesData")]
    public class SpecialShapeData : ScriptableObject
    {
        public List<SpecialShapeModel> SpacialShapes => _specialShapeModels;

        [SerializeField] private List<SpecialShapeModel> _specialShapeModels;
    }
}
