using TMPro;
using UnityEngine;

public class ThoughtDisplayManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _thoughtText;

    // Метод для отображения мысли
    public void DisplayThought(string thought)
    {
        if (_thoughtText != null)
        {
            _thoughtText.text = thought;
        }
    }
}

