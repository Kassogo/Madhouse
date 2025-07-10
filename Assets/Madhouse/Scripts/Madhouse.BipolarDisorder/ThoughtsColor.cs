using UnityEngine;

namespace Madhouse.BipolarDisorder
{
    public class ThoughtsColor : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _mainRenderer;
        [SerializeField] private SpriteRenderer _outlineRenderer;
        [SerializeField] private Sprite _whiteCircleSprite; // Новый спрайт для белого круга
        [SerializeField] private Sprite _blackCircleSprite; // Новый спрайт для черного круга

        // Публичное свойство для получения "цвета" мысли на основе её спрайта
        public Color CurrentThoughtColor
        {
            get
            {
                if (_mainRenderer != null)
                {
                    if (_mainRenderer.sprite == _whiteCircleSprite)
                        return Color.white;
                    else if (_mainRenderer.sprite == _blackCircleSprite)
                        return Color.black;
                }
                return Color.clear; // Возвращаем прозрачный цвет, если спрайт не определен
            }
        }

        private void Awake()
        {
            if (_mainRenderer == null)
            {
                _mainRenderer = GetComponent<SpriteRenderer>();
            }

            if (_outlineRenderer == null)
            {
                if (transform.childCount > 0)
                {
                    _outlineRenderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
                }
            }
        }

        public void SetSprite(Color color) // Изменяем метод на SetSprite, но принимаем Color для удобства
        {
            if (_mainRenderer == null) return;

            if (color == Color.white)
            {
                _mainRenderer.sprite = _whiteCircleSprite;
            }
            else if (color == Color.black)
            {
                _mainRenderer.sprite = _blackCircleSprite;
            }

            if (_outlineRenderer != null)
            {
                _outlineRenderer.color = Color.black; // Обводка всегда черная
            }
        }
    }
}