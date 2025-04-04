using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Вьюшка задач.
    /// </summary>
    public class TaskView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _textFirstTask;
        [SerializeField] private TextMeshProUGUI _textSecondTask;
        [SerializeField] private TextMeshProUGUI _textWrongTask;
        [Space]
        [SerializeField] private TextMeshProUGUI _textSpecialTask;
        [SerializeField] private GameObject _panelSpecialTask;

        private readonly string[] _nameColors = {"Красные", "Синие", "Желтые", "Зелёные", "Фиолетовые"};
        private readonly string[] _nameForms = {"Круги", "Треугольники", "Квадраты", "Трапеции", "Ромбы"};
        private readonly string[] _nameInteractions = {"Лопнуть", "Забрать", "Зажать", "Потрясти"};
        private readonly Dictionary<SpecialShapeTypes, string> _nameSpecialTypes 
            = new Dictionary<SpecialShapeTypes, string> { { SpecialShapeTypes.CorrectBomb, "Бомбу" }, { SpecialShapeTypes.SlowerTime, "Таймер" }, { SpecialShapeTypes.IncreaseScore, "Монетку" } };

        /// <summary>
        /// Показ задач.
        /// </summary>
        /// <param name="task"></param>
        public void ShowTasks(TaskModel task)
        {
            _textFirstTask.text = TaskConvertToString(task.FirstTask);
            _textSecondTask.text = TaskConvertToString(task.SecondTask);
            _textWrongTask.text = "Нельзя: " + TaskConvertToString(task.WrongTask);
        }

        /// <summary>
        /// Показ специальных задач.
        /// </summary>
        /// <param name="interactionEndTypes"></param>
        /// <param name="specialShapeTypes"></param>
        public void ShowSpecialTask(InteractionEndTypes interactionEndTypes, SpecialShapeTypes specialShapeTypes)
        {
            _panelSpecialTask.SetActive(true);
            _textSpecialTask.text = _nameInteractions[(int)interactionEndTypes] + " " + _nameSpecialTypes[specialShapeTypes];
        }

        /// <summary>
        /// Убрать отображение специальных задач.
        /// </summary>
        public void RemoveSpecialTask()
        {
            _panelSpecialTask.SetActive(false);
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
