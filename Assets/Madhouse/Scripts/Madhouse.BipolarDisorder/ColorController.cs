using UnityEngine;

namespace Madhouse.BipolarDisorder
{
    /// <summary>
    /// This class changes color of player from white to black 
    /// </summary>
    public class ColorController : MonoBehaviour
    {
        [SerializeField] private InputController _inputController;
        [SerializeField] private Color _positiveColor = Color.white;
        [SerializeField] private Color _negativeColor = Color.black;

        private SpriteRenderer _spriteRenderer;

        private void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _spriteRenderer.color = _positiveColor;
        }

        private void Start() => _inputController.onKeyDownAction += ChangeColor;

        private void OnDestroy() => _inputController.onKeyDownAction -= ChangeColor;

        private void ChangeColor() => _spriteRenderer.color = _spriteRenderer.color == _positiveColor ? _negativeColor : _positiveColor;
    }
}
