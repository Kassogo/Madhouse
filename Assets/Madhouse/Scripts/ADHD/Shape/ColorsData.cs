using System.Collections.Generic;
using UnityEngine;

namespace Madhouse.ADHD
{
    [CreateAssetMenu(fileName = "ColorsData", menuName = "Madhouse/ColorsData")]
    public class ColorsData : ScriptableObject
    {
        public List<ColorModel> Colors => _colors;

        [SerializeField] private List<ColorModel> _colors;
    }
}
