using UnityEngine;
using TMPro;
using DG.Tweening;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Вьюшка для показа счёта.
    /// </summary>
    public class ScoreView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _scoreText;
        [SerializeField] private SpriteRenderer _hurtEffect;

        private float _shakeDuration = 1.5f;
        private float _shakeStrength = 0.1f;

        private float _pulseAlpha = 0.2f;
        private float _pulseDuration = 0.2f;

        private Sequence _alphaSequence;

        /// <summary>
        /// Показ счёта.
        /// </summary>
        /// <param name="score"></param>
        public void ShowScore(int score)
        {
            _scoreText.text = score.ToString();
        }

        public void ShowMistake()
        {
            if (_hurtEffect == null)
                return;

            _alphaSequence?.Kill();

            _hurtEffect.gameObject.transform.DOShakePosition(
            _shakeDuration,
            _shakeStrength,
            vibrato: 50,
            randomness: 90,
            snapping: false,
            fadeOut: true
            );

            _alphaSequence = DOTween.Sequence();
            _alphaSequence.Append(_hurtEffect.DOFade(_pulseAlpha, _pulseDuration).SetEase(Ease.OutQuad));
            _alphaSequence.Append(_hurtEffect.DOFade(0f, _pulseDuration).SetEase(Ease.InQuad));


            _alphaSequence.Play();
        }
    }
}
