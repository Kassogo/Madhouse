using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Madhouse.BipolarDisorder
{
    public class ThoughtAssigner : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _staticThoughtText;
        [SerializeField] private ThoughtsSpawner _thoughtsSpawner; // Ссылка на спаунер
        [SerializeField] private Color _thoughtTextColor = Color.white; // Цвет текста мысли (назначается в инспекторе)

        private Dictionary<int, string> manicThoughts = new()
        {
            { 1, "Я гений!" }, { 2, "Новый проект!" }, { 3, "Мне не нужен сон!" },
            { 4, "Всем расскажу!" }, { 5, "Я чувствую истину!" }, { 6, "Деньги — фигня!" }
        };

        private Dictionary<int, string> depressiveThoughts = new()
        {
            { 1, "Я ничтожество." }, { 2, "Бесполезная идея." }, { 3, "Не могу встать." },
            { 4, "Меня не слушают." }, { 5, "Я ошибаюсь всегда." }, { 6, "Денег нет." }
        };

        private string _currentThought;

        private void OnEnable()
        {
            if (_thoughtsSpawner != null)
            {
                _thoughtsSpawner.OnThoughtSpawnedColor.AddListener(OnThoughtSpawned);
            }
            else
            {
                Debug.LogError("ThoughtAssigner: ThoughtsSpawner не назначен!");
            }

            // Устанавливаем цвет текста при включении скрипта
            if (_staticThoughtText != null)
            {
                _staticThoughtText.color = _thoughtTextColor;
            }
        }

        private void OnDisable()
        {
            if (_thoughtsSpawner != null)
            {
                _thoughtsSpawner.OnThoughtSpawnedColor.RemoveListener(OnThoughtSpawned);
            }
        }

        private void OnThoughtSpawned(Color spawnedColor)
        {
            if (_staticThoughtText == null)
            {
                Debug.LogError("ThoughtAssigner: _staticThoughtText не назначен!");
                return;
            }

            if (spawnedColor == Color.white)
            {
                if (manicThoughts.Count > 0)
                {
                    _currentThought = manicThoughts[Random.Range(1, manicThoughts.Count + 1)];
                }
                else
                {
                    _currentThought = "Нет маниакальных мыслей!";
                }
            }
            else if (spawnedColor == Color.black)
            {
                if (depressiveThoughts.Count > 0)
                {
                    _currentThought = depressiveThoughts[Random.Range(1, depressiveThoughts.Count + 1)];
                }
                else
                {
                    _currentThought = "Нет депрессивных мыслей!";
                }
            }

            _staticThoughtText.text = _currentThought;
            // _staticThoughtText.color = spawnedColor; // Убрали изменение цвета текста
        }
    }
}