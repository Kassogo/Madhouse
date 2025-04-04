using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private Camera _camera; // Если фон через камеру
    [SerializeField] private Color _startColor = Color.gray;
    [SerializeField] private Color _endColor = Color.white;
    [SerializeField] private float _maxScore = 100f; // При каком количестве очков фон станет светлым
    [SerializeField] private float _darkenAmount = 0.3f; // Насколько темнее становится фон при потере жизни

    private float _currentScore = 0;
    private float _currentDarkness = 0;

    private void Start()
    {
        if (_camera == null)
        {
            _camera = Camera.main;
        }

        _camera.backgroundColor = _startColor;
    }

    public void UpdateScore(float score)
    {
        _currentScore = Mathf.Clamp(score, 0, _maxScore);
        UpdateBackgroundColor();
    }

    public void OnLifeLost()
    {
        _currentDarkness += _darkenAmount; // Увеличиваем затемнение
        _currentDarkness = Mathf.Clamp01(_currentDarkness); // Ограничиваем в пределах 0-1
        UpdateBackgroundColor();
    }

    private void UpdateBackgroundColor()
    {
        float t = _currentScore / _maxScore;
        Color targetColor = Color.Lerp(_startColor, _endColor, t);
        _camera.backgroundColor = Color.Lerp(targetColor, Color.black, _currentDarkness);
    }
}
