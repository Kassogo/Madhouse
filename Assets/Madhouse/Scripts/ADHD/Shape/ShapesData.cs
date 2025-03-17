using System.Collections.Generic;
using UnityEngine;

namespace Madhouse.ADHD
{
    [CreateAssetMenu(fileName ="ShapesData", menuName ="Madhouse/ShapesData")]
    public class ShapesData : ScriptableObject
    {
        public List<ShapeModel> Shapes => _shapeModels;

        [SerializeField] private List<ShapeModel> _shapeModels;
    }
}
