using UnityEngine;
using System;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Контролер считывающий выполнение заданий и получение за это очков.
    /// </summary>
    public class ScoreController : MonoBehaviour
    {
        public event Action<int> OnChangeScore;

        [SerializeField] private TaskController _taskController;
        [SerializeField] private ShapesDispatcher _spawner;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private int _addScoreForFirstTask = 2;
        [SerializeField] private int _addScoreForSecondTask = 1;
        [SerializeField] private int _removeScoreForWrongTask = 2;
        [SerializeField] private int _removeScoreAfterChangeTask = 1;

        private int _score;

        /// <summary>
        /// Изменение счёта.
        /// </summary>
        /// <param name="changeScore"></param>
        public void AddScore(int changeScore)
        {
            _scoreView.ShowScore(_score + changeScore);
        }

        private void Start()
        {
            _spawner.OnDestroyObject += CheckDestroyObject;
            _taskController.OnChangeTask += DecreaseScore;
            SetScore(0);
        }

        private void OnDestroy()
        {
            _spawner.OnDestroyObject -= CheckDestroyObject;
            _taskController.OnChangeTask -= DecreaseScore;
        }

        private void CheckDestroyObject(ShapeTypes form, ShapeColors color, InteractionEndTypes interactionEnd)
        {
            if (interactionEnd == InteractionEndTypes.None)
                return;

            if (CheckCompledTask(_taskController.TaskChoosen.WrongTask, form, color, interactionEnd))
                SetScore(_score - _removeScoreForWrongTask);
            else if (CheckCompledTask(_taskController.TaskChoosen.FirstTask, form, color, interactionEnd))
                SetScore(_score + _addScoreForFirstTask);
            else if (CheckCompledTask(_taskController.TaskChoosen.SecondTask, form, color, interactionEnd))
                SetScore(_score + _addScoreForSecondTask);
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

        private void SetScore(int score)
        {
            _score = score;
            _scoreView.ShowScore(_score);
            OnChangeScore.Invoke(_score);
        }

        private void DecreaseScore()
        {
            SetScore(_score - _removeScoreAfterChangeTask);
        }
    }
}
