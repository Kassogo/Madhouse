using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Madhouse.BipolarDisorder
{
    public class ThoughtAssigner : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _staticThoughtText;
        [SerializeField] private ThoughtsSpawner _thoughtsSpawner; // ������ �� �������
        [SerializeField] private Color _thoughtTextColor = Color.white; // ���� ������ ����� (����������� � ����������)

        private Dictionary<int, string> manicThoughts = new()
        {
            { 1, "� �����!" }, { 2, "����� ������!" }, { 3, "��� �� ����� ���!" },
            { 4, "���� ��������!" }, { 5, "� �������� ������!" }, { 6, "������ � �����!" }
        };

        private Dictionary<int, string> depressiveThoughts = new()
        {
            { 1, "� �����������." }, { 2, "����������� ����." }, { 3, "�� ���� ������." },
            { 4, "���� �� �������." }, { 5, "� �������� ������." }, { 6, "����� ���." }
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
                Debug.LogError("ThoughtAssigner: ThoughtsSpawner �� ��������!");
            }

            // ������������� ���� ������ ��� ��������� �������
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
                Debug.LogError("ThoughtAssigner: _staticThoughtText �� ��������!");
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
                    _currentThought = "��� ������������ ������!";
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
                    _currentThought = "��� ������������ ������!";
                }
            }

            _staticThoughtText.text = _currentThought;
            // _staticThoughtText.color = spawnedColor; // ������ ��������� ����� ������
        }
    }
}