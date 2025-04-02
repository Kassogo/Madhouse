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
            { 1, "� �����!" }, { 2, "����� ������!" }, { 3, "��� �� ����� ���!" },
            { 4, "���� ��������!" }, { 5, "� �������� ������!" }, { 6, "������ � �����!" }
            // ������ ��������� �����
        };

        private Dictionary<int, string> depressiveThoughts = new Dictionary<int, string>
        {
            { 1, "� �����������." }, { 2, "����������� ����." }, { 3, "�� ���� ������." },
            { 4, "���� �� �������." }, { 5, "� �������� ������." }, { 6, "����� ���." }
            // ������ ��������� �����
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
