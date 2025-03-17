using System;
using UnityEngine;

namespace Madhouse.ADHD
{
    [Serializable]
    public class ColorModel
    {
        public ShapeColors ColorType => _shapeColor;

        public Color ColorValue => _color;

        [SerializeField] private ShapeColors _shapeColor;
        [SerializeField] private Color _color;
    }
}