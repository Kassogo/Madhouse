using UnityEngine;
using System;

namespace Madhouse.ADHD
{
    /// <summary>
    /// Контролер считывающий выполнение заданий и получение за это очков.
    /// </summary>
    public class ScoreController : MonoBehaviour
    {
        public event Action<int> OnChangeScore = delegate { };

        [SerializeField] private TaskController _taskController;
        [SerializeField] private ShapesDispatcher _spawner;
        [SerializeField] private ScoreView _scoreView;
        [Space]
        [SerializeField] private ScoreSetting _setting;

        private int _score;
        private bool _isMadeTask = false;

        /// <summary>
        /// Изменение счёта.
        /// </summary>
        /// <param name="changeScore"></param>
        public void AddScore(int changeScore)
        {
            SetScore(_score + changeScore);
        }

        private void Start()
        {
            _spawner.OnDestroyObject += CheckDestroyObject;
            _taskController.OnChangeTask += CheckMadeTask;
            SetScore(0);
        }

        private void OnDestroy()
        {
            _spawner.OnDestroyObject -= CheckDestroyObject;
            _taskController.OnChangeTask -= CheckMadeTask;
        }

        private void CheckDestroyObject(ShapeTypes form, ShapeColors color, InteractionEndTypes interactionEnd)
        {
            if (interactionEnd == InteractionEndTypes.None)
                return;

            if (CheckCompledTask(_taskController.TaskChoosen.WrongTask, form, color, interactionEnd))
                SetScore(_score - _setting.RemoveScoreForCompletWrongTask);
            else if (CheckCompledTask(_taskController.TaskChoosen.FirstTask, form, color, interactionEnd))
            {
                _isMadeTask = true;
                SetScore(_score + _setting.AddScoreForCompletFirstTask);
            }
            else if (CheckCompledTask(_taskController.TaskChoosen.SecondTask, form, color, interactionEnd))
            {
                _isMadeTask = true;
                SetScore(_score + _setting.AddScoreForCompletSecondTask);
            }
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

        private void CheckMadeTask()
        {
            if (!_isMadeTask)
                SetScore(_score - _setting.RemoveScoreForAfterChangeTask);
            _isMadeTask = false;
        }
    }
}
