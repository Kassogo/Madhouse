using UnityEngine;

/// <summary>
/// Manages the background color based on the player's score and life status.
/// The color transitions from dark gray to white as the score increases,
/// and darkens temporarily when the player loses a life.
/// </summary>
public class BackgroundManager : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Color _startColor = Color.gray;
    [SerializeField] private Color _endColor = Color.white;
    [SerializeField] private float _maxScore = 100f; 
    [SerializeField] private float _darkenAmount = 0.3f; 

    private float _currentScore = 0;
    private float _currentDarkness = 0;

    private void Start()
    {
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
