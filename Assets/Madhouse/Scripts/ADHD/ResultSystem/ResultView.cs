using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Вьюшка для показа результата игры.
    /// </summary>
    public class ResultView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textResult;
        [SerializeField] private string _winText;
        [SerializeField] private string _loseText;
        [SerializeField] private Image _blackVeil;
        [Space]
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Sprite[] _backgroundSprites;

        private Color _colorVeil;
        private int _indexSpriteBackground = 0;
        private int _needSpriteBackground =0;
        private float _needCoefficientBackground;
        private float _timer;
        private float _timeNextChangeBackground = .5f;

        /// <summary>
        /// Показ экрана победы.
        /// </summary>
        public void ShowWinWindow()
        {
            _textResult.gameObject.SetActive(true);
            _textResult.text = _winText;
        }

        /// <summary>
        /// Показ экрана проигрыша.
        /// </summary>
        public void ShowLoseWindow()
        {
            _textResult.gameObject.SetActive(true);
            _blackVeil.gameObject.SetActive(false);
            _textResult.text = _loseText;
        }

        /// <summary>
        /// Изменение видимости чёрной пелены.
        /// </summary>
        /// <param name="visibility"></param>
        public void ChangeVisibilityVeil(float visibility)
        {
            _colorVeil = Color.black;
            _colorVeil.a = visibility;
            _blackVeil.DOColor(_colorVeil, 2f);
        }

        /// <summary>
        /// Изменение фона в соответствии с счётом.
        /// </summary>
        /// <param name="score"></param>
        public void ChangeBackground(int score, int maxScore)
        {
            if(score <= 0)
            {
                _needSpriteBackground = 0;
                return;
            }
            if(score >= maxScore)
            {
                _needSpriteBackground = _backgroundSprites.Length;
                return;
            }
            _needCoefficientBackground = ((float)score) / maxScore;
            _needSpriteBackground = Mathf.FloorToInt(((float)_backgroundSprites.Length) * _needCoefficientBackground);
        }

        private void Update()
        {
            if (_needSpriteBackground != _indexSpriteBackground && Time.time > _timer)
                ChangeSprites();
        }

        private void ChangeSprites()
        {
            _timer = Time.time + _timeNextChangeBackground;
            _indexSpriteBackground += _needSpriteBackground > _indexSpriteBackground ? 1 : -1;
            _spriteRenderer.sprite = _backgroundSprites[_indexSpriteBackground];
        }
    }
}