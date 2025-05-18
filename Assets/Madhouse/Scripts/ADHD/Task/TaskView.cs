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
        [SerializeField] private TaskLine _lineFirstTask;
        [SerializeField] private TaskLine _lineSecondTask;
        [SerializeField] private TaskLine _lineWrongTask;
        [Space]
        [SerializeField] private TaskLine _lineSpecialTask;
        [SerializeField] private GameObject _panelSpecialTask;
        [Space]
        [SerializeField] private SpecialShapeData _specialShapeData;
        [SerializeField] private TaskSetting _taskSettings;

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
            TaskConvertToPictures(task.FirstTask, _lineFirstTask);
            TaskConvertToPictures(task.SecondTask, _lineSecondTask);
            TaskConvertToPictures(task.WrongTask, _lineWrongTask);
        }

        /// <summary>
        /// Показ специальных задач.
        /// </summary>
        /// <param name="interactionEndTypes"></param>
        /// <param name="specialShapeTypes"></param>
        public void ShowSpecialTask(InteractionEndTypes interactionEndTypes, SpecialShapeTypes specialShapeTypes)
        {
            _panelSpecialTask.SetActive(true);
            _lineSpecialTask.SetPictures(_taskSettings.GetSpriteInteraction(interactionEndTypes), _specialShapeData.GetSpacialShapes(specialShapeTypes));
        }

        /// <summary>
        /// Убрать отображение специальных задач.
        /// </summary>
        public void RemoveSpecialTask()
        {
            _panelSpecialTask.SetActive(false);
        }

        private void TaskConvertToPictures(Task task, TaskLine taskLine)
        {
            switch (task.Type)
            {
                case TaskType.Color:
                    taskLine.SetPictures(null, _taskSettings.BlobSprite);
                    taskLine.SetColor(_taskSettings.GetColor(task.ShapeColor));
                    break;
                case TaskType.Form:
                    taskLine.SetPictures(null, _taskSettings.GetForm(task.Form));
                    break;
                case TaskType.Interaction:
                    taskLine.SetPictures(_taskSettings.GetSpriteInteraction(task.Interaction));
                    break;
                case TaskType.InteractionAndColor:
                    taskLine.SetPictures(_taskSettings.GetSpriteInteraction(task.Interaction), _taskSettings.BlobSprite);
                    taskLine.SetColor(_taskSettings.GetColor(task.ShapeColor));
                    break;
                case TaskType.InteractionAndForm:
                    taskLine.SetPictures(_taskSettings.GetSpriteInteraction(task.Interaction), _taskSettings.GetForm(task.Form));
                    break;
                case TaskType.ColorAndForm:
                    taskLine.SetPictures(null, _taskSettings.GetForm(task.Form));
                    taskLine.SetColor(_taskSettings.GetColor(task.ShapeColor));
                    break;
                default:
                    Debug.LogError("Не найдено отображение этого типа задач");
                    break;
            }
        }

    }
}
