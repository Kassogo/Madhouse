using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Madhouse.ADHD
{
    public class TaskView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textFirstTask;
        [SerializeField] private TextMeshProUGUI _textSecondTask;
        [SerializeField] private TextMeshProUGUI _textWrongTask;

        private string[] _nameColors = {"Красные", "Синие", "Желтые", "Зелёные", "Фиолетовые"};
        private string[] _nameForms = {"Круги", "Треугольники", "Квадраты", "Трапеции", "Ромбы"};
        private string[] _nameInteractions = {"Лопнуть", "Забрать", "Зажать", "Потрясти"};

        public void ShowTasks(TaskModel task)
        {
            _textFirstTask.text = TaskConvertToString(task.FirstTask);
            _textSecondTask.text = TaskConvertToString(task.SecondTask);
            _textWrongTask.text = "Нельзя: " + TaskConvertToString(task.WrongTask);
        }

        private string TaskConvertToString(Task task)
        {
            switch (task.Type)
            {
                case TaskType.Color:
                    return _nameColors[(int)task.ShapeColor];
                case TaskType.Form:
                    return _nameForms[(int)task.Form];
                case TaskType.Interaction:
                    return _nameInteractions[(int)task.Interaction];
                case TaskType.InteractionAndColor:
                    return _nameInteractions[(int)task.Interaction] + " " + _nameColors[(int)task.ShapeColor];
                case TaskType.InteractionAndForm:
                    return _nameInteractions[(int)task.Interaction] + " " + _nameForms[(int)task.Form];
                case TaskType.ColorAndForm:
                    return _nameColors[(int)task.ShapeColor] + " " + _nameForms[(int)task.Form];
                default:
                    return "Strange type task!";
            }
        }
    }
}
