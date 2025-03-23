using System;
using UnityEngine;

namespace Madhouse.ADHD
{
    [Serializable]
    public class SpecialShapeModel
    {
        [SerializeField] private SpecialShapeTypes _type;
        [SerializeField] private Sprite _sprite;

        public SpecialShapeTypes Type => _type;

        public Sprite Picture => _sprite;
    }
}
