using UnityEngine;

namespace Madhouse.ADHD
{
    [System.Serializable]
    public class ShapeModel
    {
        public ShapeTypes Type => _shapeType;
        public Sprite Picture => _shapePicture;
        public Sprite Glare => _glarePicture;

        [SerializeField] private Sprite _shapePicture;
        [SerializeField] private Sprite _glarePicture;
        [SerializeField] private ShapeTypes _shapeType;
    }
}