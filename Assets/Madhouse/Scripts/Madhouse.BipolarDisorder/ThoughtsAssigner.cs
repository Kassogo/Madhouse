using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Madhouse.BipolarDisorder
{
    public class ThoughtAssigner : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _thoughtText;
        [SerializeField] private SpriteRenderer _circleRenderer;

        private Dictionary<int, string> manicThoughts = new Dictionary<int, string>
        {
            { 1, "Я гений!" }, { 2, "Новый проект!" }, { 3, "Мне не нужен сон!" },
            { 4, "Всем расскажу!" }, { 5, "Я чувствую истину!" }, { 6, "Деньги — фигня!" }
            // Добавь остальные мысли
        };

        private Dictionary<int, string> depressiveThoughts = new Dictionary<int, string>
        {
            { 1, "Я ничтожество." }, { 2, "Бесполезная идея." }, { 3, "Не могу встать." },
            { 4, "Меня не слушают." }, { 5, "Я ошибаюсь всегда." }, { 6, "Денег нет." }
            // Добавь остальные мысли
        };

        private void Start()
        {
            AssignThought();
        }

        private void AssignThought()
        {
            if (_circleRenderer == null || _thoughtText == null) return;

            Color color = _circleRenderer.color;
            string randomThought = "";

            if (color == Color.white)
            {
                randomThought = manicThoughts[Random.Range(1, manicThoughts.Count + 1)];
            }
            else if (color == Color.black)
            {
                randomThought = depressiveThoughts[Random.Range(1, depressiveThoughts.Count + 1)];
            }

            _thoughtText.text = randomThought;
        }
    }
}
