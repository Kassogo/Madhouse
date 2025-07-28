using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimerManager : MonoBehaviour
{
    [Header("Timer Settings")]
    [SerializeField] private float _maxTime = 15f; // Максимальное время игры в секундах
    [SerializeField] private float _timeToAddCorrect = 2.5f; // Время, добавляемое за правильный ответ
    [SerializeField] private float _timeToRemoveWrong = 1.5f; // Время, отнимаемое за неправильный ответ

    [Header("UI Elements")]
    [SerializeField] private Image _timeBar; // Полоса времени (UI Image)
    [SerializeField] private GameObject _gameOverMenu; // GameObject меню проигрыша
    [SerializeField] private TextMeshProUGUI _timerText; // Текст для отображения времени (опционально)

    private float _currentTime; // Текущее время
    private bool _isTimerRunning = false; // Флаг, идет ли таймер

    public static TimerManager Instance { get; private set; } // Singleton

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogWarning("Найден еще один экземпляр TimerManager. Уничтожаем дубликат.", this.gameObject);
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        _currentTime = _maxTime; // Инициализируем текущее время максимальным
        UpdateTimeBar();
        UpdateTimeText(); // Обновляем текст таймера при старте
        StartTimer(); // Запускаем таймер сразу при старте игры
        if (_gameOverMenu != null) _gameOverMenu.SetActive(false); // Убедимся, что меню проигрыша скрыто в начале
    }

    void Update()
    {
        if (_isTimerRunning)
        {
            _currentTime -= Time.deltaTime; // Уменьшаем время каждую секунду

            if (_currentTime <= 0f)
            {
                _currentTime = 0f; // Чтобы время не уходило в минус
                _isTimerRunning = false; // Останавливаем таймер
                OnTimeOut(); // Вызываем метод, когда время истекло (ИЗМЕНЕНО)
            }

            UpdateTimeBar(); // Обновляем полосу времени
            UpdateTimeText(); // Обновляем текст таймера
        }
    }

    // Запустить таймер
    public void StartTimer()
    {
        _isTimerRunning = true;
    }

    // Остановить таймер
    public void StopTimer()
    {
        _isTimerRunning = false;
    }

    // Добавить время за правильный ответ
    public void AddTime()
    {
        _currentTime += _timeToAddCorrect;
        if (_currentTime > _maxTime) _currentTime = _maxTime; // Не даем времени превысить максимум
        UpdateTimeBar();
        UpdateTimeText();
    }

    // Отнять время за неправильный ответ
    public void RemoveTime()
    {
        _currentTime -= _timeToRemoveWrong;
        if (_currentTime < 0f) _currentTime = 0f; // Время не должно быть отрицательным
        UpdateTimeBar();
        UpdateTimeText();
        if (_currentTime <= 0f && _isTimerRunning) // Проверяем, не истекло ли время после вычитания
        {
            _isTimerRunning = false;
            OnTimeOut(); // Вызываем метод, когда время истекло после ошибки (ИЗМЕНЕНО)
        }
    }

    // Метод вызывается, когда время истекло
    private void OnTimeOut() // ИЗМЕНЕНО НА OnTimeOut()
    {
        Debug.Log("Время вышло!");
        StopTimer(); // Убедимся, что таймер остановлен
        if (_gameOverMenu != null)
        {
            _gameOverMenu.SetActive(true); // Показываем меню проигрыша
        }
        Time.timeScale = 0; // Ставим игру на паузу (замораживаем время)
    }

    // Обновление полосы времени (UI Image Fill Amount)
    private void UpdateTimeBar()
    {
        if (_timeBar != null)
        {
            _timeBar.fillAmount = _currentTime / _maxTime; // Заполняем полосу в зависимости от оставшегося времени
        }
        else
        {
            Debug.LogWarning("TimeBar Image не назначен в TimerManager!", this.gameObject);
        }
    }

    // Обновление текстового отображения времени (опционально)
    private void UpdateTimeText()
    {
        if (_timerText != null)
        {
            _timerText.text = Mathf.RoundToInt(_currentTime).ToString(); // Отображаем целое число секунд
        }
    }

    // Перезапуск таймера (для новой игры или рестарта)
    public void ResetTimer()
    {
        _currentTime = _maxTime;
        UpdateTimeBar();
        UpdateTimeText();
        if (_gameOverMenu != null) _gameOverMenu.SetActive(false); // Скрываем меню проигрыша при рестарте
        Time.timeScale = 1; // Восстанавливаем нормальное течение времени
        StartTimer(); // Запускаем таймер заново
    }
}