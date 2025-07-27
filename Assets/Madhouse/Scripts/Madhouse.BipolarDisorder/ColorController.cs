using UnityEngine;

namespace Madhouse.BipolarDisorder
{
    public class ColorController : MonoBehaviour
    {
        [SerializeField] private InputController _inputController;
        [SerializeField] private Sprite _whitePlayerSprite;
        [SerializeField] private Sprite _blackPlayerSprite;

        private SpriteRenderer _spriteRenderer;
        private bool _isPositiveColor = true;

        // Новое публичное свойство для получения текущего "цвета" игрока
        public Color CurrentPlayerColor
        {
            get
            {
                return _isPositiveColor ? Color.white : Color.black;
            }
        }

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.sprite = _whitePlayerSprite;
            _isPositiveColor = true;
        }

        private void Start() => _inputController.onKeyDownAction += ChangeColor;

        private void OnDestroy() => _inputController.onKeyDownAction -= ChangeColor;

        private void ChangeColor()
        {
            _isPositiveColor = !_isPositiveColor;
            _spriteRenderer.sprite = _isPositiveColor ? _whitePlayerSprite : _blackPlayerSprite;
        }
    }
}