using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Вьюшка фигуры.
    /// </summary>
    public class ShapeView : MonoBehaviour
    {
        [SerializeField] private ShapesData _shapesData;
        [SerializeField] private ColorsData _colorsData;
        [SerializeField] private SpriteRenderer _spriteShape;
        [SerializeField] private SpriteRenderer _spriteGlare;
        [SerializeField] private SpriteRenderer _spriteShadow;

        /// <summary>
        /// Смена спрайта и цвета с соответствием с типами.
        /// </summary>
        /// <param name="shape"></param>
        /// <param name="color"></param>
        public void ShowType(ShapeTypes shape, ShapeColors color)
        {
            SetSprite(shape);
            SetColor(color);
        }

        private void SetSprite(ShapeTypes shape)
        {
            for (int i = 0; i < _shapesData.Shapes.Count; i++)
            {
                if (_shapesData.Shapes[i].Type == shape)
                {
                    _spriteGlare.sprite = _shapesData.Shapes[i].Glare;
                    _spriteShape.sprite = _shapesData.Shapes[i].Picture;
                    _spriteShadow.sprite = _shapesData.Shapes[i].Shadow;
                    break;
                }
            }
        }

        private void SetColor(ShapeColors color)
        {
            for (int i = 0; i < _colorsData.Colors.Count; i++)
            {
                if (_colorsData.Colors[i].ColorType == color)
                {
                    _spriteShape.color = _colorsData.Colors[i].ColorValue;
                    break;
                }
            }
        }
    }
}
