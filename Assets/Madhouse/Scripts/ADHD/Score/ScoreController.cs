using UnityEngine;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Контролер считывающий выполнение заданий и получение за это очков.
    /// </summary>
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private TaskController _taskController;
        [SerializeField] private ShapesController _spawner;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private int _addScoreForFirstTask = 2;
        [SerializeField] private int _addScoreForSecondTask = 1;
        [SerializeField] private int _removeScoreForWrongTask = 2;

        private int _score;

        /// <summary>
        /// Изменение счёта.
        /// </summary>
        /// <param name="changeScore"></param>
        public void ChangeScore(int changeScore)
        {
            _score += changeScore;
            _scoreView.SetScore(_score);
        }

        private void Awake()
        {
            _spawner.OnDestroyObject += CheckDestroyObject;
            _score = 0;
            _scoreView.SetScore(_score);
        }

        private void OnDestroy()
        {
            _spawner.OnDestroyObject -= CheckDestroyObject;
        }

        private void CheckDestroyObject(ShapeTypes form, ShapeColors color, InteractionEndTypes interactionEnd)
        {
            if (CheckCompledTask(_taskController.TaskChoosen.WrongTask, form, color, interactionEnd))
                _score -= _removeScoreForWrongTask;
            else if (CheckCompledTask(_taskController.TaskChoosen.FirstTask, form, color, interactionEnd))
                _score += _addScoreForFirstTask;
            else if (CheckCompledTask(_taskController.TaskChoosen.SecondTask, form, color, interactionEnd))
                _score += _addScoreForSecondTask;

            _scoreView.SetScore(_score);
        }

        private bool CheckCompledTask(Task task, ShapeTypes form, ShapeColors color, InteractionEndTypes interactionEnd)
        {
            switch (task.Type)
            {
                case TaskType.Color:
                    return task.ShapeColor == color;
                case TaskType.Form:
                    return task.Form == form;
                case TaskType.Interaction:
                    return task.Interaction == interactionEnd;
                case TaskType.InteractionAndColor:
                    return task.Interaction == interactionEnd && task.ShapeColor == color;
                case TaskType.InteractionAndForm:
                    return task.Interaction == interactionEnd && task.Form == form;
                case TaskType.ColorAndForm:
                    return task.Form == form && task.ShapeColor == color;
                default:
                    return false;
            }
        }
    }
}
