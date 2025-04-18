using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Вьюшка для специальной фигуры.
    /// </summary>
    public class SpecialShapeView : MonoBehaviour
    {
        [SerializeField] private SpecialShapeData _specialShapeData;
        [SerializeField] private SpriteRenderer _spriteShape;

        /// <summary>
        /// Смена спрайта с соответствием с типом.
        /// </summary>
        /// <param name="shape"></param>
        public void ShowType(SpecialShapeTypes shape)
        {
            for (int i = 0; i < _specialShapeData.SpacialShapes.Count; i++)
            {
                if (_specialShapeData.SpacialShapes[i].Type == shape)
                {
                    _spriteShape.sprite = _specialShapeData.SpacialShapes[i].Picture;
                    break;
                }
            }
        }
    }
}
