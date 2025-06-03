using System.Collections.Generic;
using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// SO со зачением цветов для фигур.
    /// </summary>
    [CreateAssetMenu(fileName = "ColorsData", menuName = "Madhouse/ADHD/ColorsData")]
    public class ColorsData : ScriptableObject
    {
        [SerializeField] private List<ColorModel> _colors;

        /// <summary>
        /// Цвета фигур.
        /// </summary>
        public List<ColorModel> Colors => _colors;
    }
}
