using UnityEngine;
using DG.Tweening;
using TMPro;

namespace Madhouse.ADHD
{
    /// <summary>
    /// ����� ������������ �����.
    /// </summary>
    public class ShowerTimeToNextTask : MonoBehaviour
    {
        [SerializeField] private float _timeStartShow = 3f;
        [SerializeField] private TextMeshProUGUI _textNumber;
        [SerializeField] private TaskController _taskController;

        private Sequence _scaleSequence;
        private bool _isAnimation = false;

        private void Awake()
        {
            _scaleSequence = DOTween.Sequence();
            _scaleSequence.SetAutoKill(false);
            for (int i = 0; i < _timeStartShow; i++)
            {
                _scaleSequence.Append(_textNumber.gameObject.transform.DOScale(0.5f, 0f));
                _scaleSequence.Append(_textNumber.gameObject.transform.DOScale(2f, 1f).SetEase(Ease.InOutQuad));
                _scaleSequence.Append(_textNumber.gameObject.transform.DOScale(0.5f, 0f));
            }

            _scaleSequence.OnComplete(() =>
            {
                _textNumber.gameObject.SetActive(false);
                _isAnimation = false;

            });
        }

        private void Update()
        {
            if (_taskController.TimeToNextTask <= _timeStartShow && _taskController.TimeToNextTask > 0)
                ShowTime();
        }

        private void OnDestroy()
        {
            _scaleSequence.Kill();
        }

        private void ShowTime()
        {
            if (!_isAnimation)
                AnimateShow();
            _textNumber.text = _taskController.TimeToNextTask.ToString("F2");
        }

        private void AnimateShow()
        {
            _isAnimation = true;
            _textNumber.gameObject.SetActive(true);
            _scaleSequence.Restart();
        }
    }
}
